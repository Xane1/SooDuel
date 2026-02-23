using UnityEditor;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    public GameOverScreen WinScreen;
    public RhythmAudioScript RhythmAudioScript;

    public int winScore = 12999;

    bool gameEnded = false;

    void Update()
    {
        if (gameEnded) return;

        if (ScoreManager.instance.P1score >= winScore && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
        {
            WinGame();
        }
        else if (ScoreManager.instance.P1score < winScore && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
        {
            
            GameOver();
        }
    }
    private IEnumerator HandleGameOver()
    {
        yield return new WaitForSecondsRealtime(1f);
        GameOverScreen.Setup(ScoreManager.instance.P1score);
        Time.timeScale = 0f;
        DestroyAllBeats();
    }
    
    private IEnumerator HandleWinGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        WinScreen.Setup(ScoreManager.instance.P1score);
        Time.timeScale = 0f;
        DestroyAllBeats();
    }
    public void GameOver()
    {
        if (gameEnded) return;

        gameEnded = true;
        StartCoroutine(HandleGameOver());
    }

    void WinGame()
    {
        if (gameEnded) return;

        gameEnded = true;
        StartCoroutine(HandleWinGame());
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