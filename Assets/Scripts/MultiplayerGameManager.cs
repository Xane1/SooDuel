using UnityEditor;
using UnityEngine;
using System.Collections;

public class MultiplayerGameManager : MonoBehaviour
{
    public GameOverScreen P2WinScreen;
    public GameOverScreen P1WinScreen;
    public RhythmAudioScript RhythmAudioScript;

    public int winScore = 12999;

    bool gameEnded = false;

    void Update()
    {
        if (gameEnded) return;

        if (ScoreManager.instance.P1score > ScoreManager.instance.P2score && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
        {
            P1WinGame();
        }
        else if (ScoreManager.instance.P1score < ScoreManager.instance.P2score && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
        {
            P2WinGame();
        }
        
    }
    private IEnumerator HandleP2WinGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        P2WinScreen.Setup(ScoreManager.instance.P2score);
        Time.timeScale = 0f;
        DestroyAllBeats();
    }
    
    private IEnumerator HandleP1WinGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        P1WinScreen.Setup(ScoreManager.instance.P1score);
        Time.timeScale = 0f;
        DestroyAllBeats();
    }
    public void P2WinGame()
    {
        if (gameEnded) return;

        gameEnded = true;
        StartCoroutine(HandleP2WinGame());
    }

    void P1WinGame()
    {
        if (gameEnded) return;

        gameEnded = true;
        StartCoroutine(HandleP1WinGame());
    }

    void DestroyAllBeats()
    {
        GameObject[] notes = GameObject.FindGameObjectsWithTag("BeatTarget");

        foreach (GameObject note in notes)
        {
            Destroy(note);
        }
    }
}