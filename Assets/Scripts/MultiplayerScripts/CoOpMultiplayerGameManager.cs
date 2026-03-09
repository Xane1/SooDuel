using UnityEngine;
using System.Collections;
using GameControllerScripts;
using UnityEngine.InputSystem;
public class CoOpMultiplayerGameManager : MonoBehaviour
{
    public GameOverScreen FailScreen;
    public GameOverScreen WinScreen;
    public RhythmAudioScript RhythmAudioScript;
    
    public GameObject MusicPlayer;
    public GameObject MusicBeatPlayer;
    public GameObject BeatSpawner;

    public GameObject PlayerMessage;
    public GameObject Tutorial;
    public GameObject P1ScoreKeeper;
    public GameObject P2ScoreKeeper;
    
    public int winScore = 10000;

    bool gameEnded = false;
    
    
    bool activated = false;
    bool activated2 = false;

    // Update is called once per frame
    void Update()
    {
        if ((!activated && Gamepad.current != null && Gamepad.current.leftShoulder.wasPressedThisFrame))
        {
            Tutorial.SetActive(false);
            P1ScoreKeeper.SetActive(true);
            activated = true;
            PlayerMessage.SetActive(true);
        }
        if ((!activated2 && GameObject.FindGameObjectsWithTag("Player2").Length > 0))
        {
            MusicPlayer.SetActive(true);
            MusicBeatPlayer.SetActive(true);
            BeatSpawner.SetActive(true);
            P2ScoreKeeper.SetActive(true);
            PlayerMessage.SetActive(false);
            
            activated2 = true;
        }
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
