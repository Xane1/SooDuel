using UnityEditor;
using UnityEngine;
using System.Collections;
using GameControllerScripts;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    public GameOverScreen WinScreen;
    public RhythmAudioScript RhythmAudioScript;
    
    public GameObject MusicPlayer;
    public GameObject MusicBeatPlayer;
    public GameObject BeatSpawner;
    
    public GameObject Tutorial;
    public GameObject ScoreKeeper;

    public int winScore = 12999;

    bool gameEnded = false;
    
    bool activated = false;
   
    
    void Update()
    {
        if ((!activated && Gamepad.current != null && Gamepad.current.leftShoulder.wasPressedThisFrame))
        {
            MusicPlayer.SetActive(true);
            MusicBeatPlayer.SetActive(true);
            BeatSpawner.SetActive(true);
            Tutorial.SetActive(false);
            ScoreKeeper.SetActive(true);
            
            activated = true;
        }
        else if ((!activated && Pointer.current.press.wasPressedThisFrame))
        {
            MusicPlayer.SetActive(true);
            MusicBeatPlayer.SetActive(true);
            BeatSpawner.SetActive(true);
            Tutorial.SetActive(false);
            ScoreKeeper.SetActive(true);
            
            activated = true;
        }
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