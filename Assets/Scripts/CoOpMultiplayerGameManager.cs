using UnityEngine;
using System.Collections;
using GameControllerScripts;
public class CoOpMultiplayerGameManager : MonoBehaviour
{
    public GameOverScreen FailScreen;
    public GameOverScreen WinScreen;
    public RhythmAudioScript RhythmAudioScript;

    public int winScore = 10000;

    bool gameEnded = false;

    // Update is called once per frame
    void Update()
    {
        if (gameEnded) return;

        if (ScoreManager.instance.P1score + ScoreManager.instance.P2score >= winScore && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
        {
            WinGame();
        }
        else if (ScoreManager.instance.P1score + ScoreManager.instance.P2score < winScore && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
        {
            
            GameOver();
        }
    }
    private IEnumerator HandleGameOver()
    {
        yield return new WaitForSecondsRealtime(1f);
        FailScreen.Setup(ScoreManager.instance.totalScore);
        Time.timeScale = 0f;
        DestroyAllBeats();
    }
    
    private IEnumerator HandleWinGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        WinScreen.Setup(ScoreManager.instance.totalScore);
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
