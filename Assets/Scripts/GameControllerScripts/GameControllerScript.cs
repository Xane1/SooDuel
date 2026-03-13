using UnityEditor;
using UnityEngine;
using System.Collections;
using GameControllerScripts;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    public RhythmAudioScript RhythmAudioScript;
    
    public GameObject MusicPlayer;
    public GameObject MusicBeatPlayer;
    public GameObject BeatSpawner;
    
    public GameObject Tutorial;
    public GameObject ScoreKeeper;

    public int easyWinScore = 7000;
    public int normalWinScore = 12999;
    public int hardWinScore = 18000;

    bool gameEnded = false;
    
    bool activated = false;
   
    
    void Update()
    {
      //Tutorial Screen
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
        
        if (RhythmAudioScript == null || RhythmAudioScript.musicSource == null || RhythmAudioScript.musicSource.clip == null)
            return;

        //Win Conditions
        if (DifficultyManager.Instance != null)
        {
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Easy)
            {
                if (ScoreManager.instance.P1score >= easyWinScore && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
                {
                    WinGame();
                }
                else if (ScoreManager.instance.P1score < easyWinScore && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
                {
            
                    LoseGame();
                }
            }
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Normal)
            {
                if (ScoreManager.instance.P1score >= normalWinScore && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
                {
                    WinGame();
                }
                else if (ScoreManager.instance.P1score < normalWinScore && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
                {
            
                    LoseGame();
                }
            }
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Hard)
            {
                if (ScoreManager.instance.P1score >= hardWinScore && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
                {
                    WinGame();
                }
                else if (ScoreManager.instance.P1score < hardWinScore && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
                {
            
                    LoseGame();
                }
            }
        }
       
    }
    private IEnumerator HandleLoseGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        GameOverScreen.Setup(ScoreManager.instance.P1score);
        GameObject lossText = GameObject.Find("GameOverBackground").transform.Find("Loss Text").gameObject;
        lossText.SetActive(true);
        Time.timeScale = 0f;
        DestroyAllBeats();
    }
    
    private IEnumerator HandleWinGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        GameOverScreen.Setup(ScoreManager.instance.P1score);
        GameObject winText = GameObject.Find("GameOverBackground").transform.Find("Win Text").gameObject;
        winText.SetActive(true);
        Time.timeScale = 0f;
        DestroyAllBeats();
    }
    void LoseGame()
    {
        if (gameEnded) return;

        gameEnded = true;
        StartCoroutine(HandleLoseGame());
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