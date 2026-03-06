using UnityEngine;
using System.Collections;
using GameControllerScripts;
using UnityEngine.InputSystem;
public class MultiplayerGameManager : MonoBehaviour
{
    public GameOverScreen P2WinScreen;
    public GameOverScreen P1WinScreen;
    public GameOverScreen DrawScreen;
    public RhythmAudioScript RhythmAudioScript;

    public GameObject MusicPlayer;
    public GameObject MusicBeatPlayer;
    public GameObject BeatSpawner;
    
    public GameObject Tutorial;
    public GameObject P1ScoreKeeper;
    public GameObject P2ScoreKeeper;
    
    public int winScore = 12999;

    bool gameEnded = false;

    bool activated = false;
    bool activated2 = false;
    void Update()
    {
        if ((!activated && Gamepad.current != null && Gamepad.current.leftShoulder.wasPressedThisFrame))
        {
            Tutorial.SetActive(false);
            P1ScoreKeeper.SetActive(true);
            activated = true;
        }
        if ((!activated2 && GameObject.FindGameObjectsWithTag("Player2").Length > 0))
        {
            MusicPlayer.SetActive(true);
            MusicBeatPlayer.SetActive(true);
            BeatSpawner.SetActive(true);
            P2ScoreKeeper.SetActive(true);
            
            activated2 = true;
        }

        if (gameEnded) return;

        if (ScoreManager.instance.P1score > ScoreManager.instance.P2score && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
        {
            P1WinGame();
        }
        else if (ScoreManager.instance.P1score < ScoreManager.instance.P2score && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
        {
            P2WinGame();
        }
        else if (ScoreManager.instance.P1score == ScoreManager.instance.P2score && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
        {
            DrawGame();
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

    private IEnumerator HandleDrawGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        DrawScreen.gameObject.SetActive(true);
        Time.timeScale = 0f;
        DestroyAllBeats();
    }

    void DrawGame()
    {
        if (gameEnded) return;
        gameEnded = true;
        StartCoroutine(HandleDrawGame());
    }
    void P2WinGame()
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