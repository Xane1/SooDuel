using UnityEngine;
using System.Collections;
using GameControllerScripts;
using UnityEngine.InputSystem;
using System.Linq;
public class MultiplayerGameManager : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    public RhythmAudioScriptAlt RhythmAudioScriptAlt;

    public GameObject MusicPlayer;
    public GameObject MusicBeatPlayer;
    public GameObject BeatSpawner;
    
    public GameObject PlayerMessage;
    public GameObject Tutorial;
    public GameObject P1ScoreKeeper;
    public GameObject P2ScoreKeeper;
    
    public int winScore = 12999;

    bool gameEnded = false;

    bool activated = false;
    bool activated2 = false;
    void Update()
    {
        if ((!activated && Gamepad.all.Any(g => g.leftShoulder.wasPressedThisFrame)))
        {
            Tutorial.SetActive(false);
            P1ScoreKeeper.SetActive(true);
            activated = true;
            PlayerMessage.SetActive(true);
            FindObjectOfType<MultiplayerManager>().EnableJoining();
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

        if (ScoreManager.instance.P1score > ScoreManager.instance.P2score && RhythmAudioScriptAlt.songPosition >= RhythmAudioScriptAlt.musicSource.clip.length)
        {
            P1WinGame();
        }
        else if (ScoreManager.instance.P1score < ScoreManager.instance.P2score && RhythmAudioScriptAlt.songPosition >= RhythmAudioScriptAlt.musicSource.clip.length)
        {
            P2WinGame();
        }
        else if (ScoreManager.instance.P1score == ScoreManager.instance.P2score && RhythmAudioScriptAlt.songPosition >= RhythmAudioScriptAlt.musicSource.clip.length)
        {
            DrawGame();
        }
        
    }
    private IEnumerator HandleP2WinGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        GameOverScreen.Setup(ScoreManager.instance.P2score);
        GameObject p2WinText = GameObject.Find("GameOverBackground").transform.Find("Player2 WinText").gameObject;
        p2WinText.SetActive(true);
        Time.timeScale = 0f;
        DestroyAllBeats();
    }
    
    private IEnumerator HandleP1WinGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        GameOverScreen.Setup(ScoreManager.instance.P1score);
        GameObject p1WinText = GameObject.Find("GameOverBackground").transform.Find("Player1 WinText").gameObject;
        p1WinText.SetActive(true);
        Time.timeScale = 0f;
        DestroyAllBeats();
    }

    private IEnumerator HandleDrawGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        GameObject drawText = GameObject.Find("GameOverBackground").transform.Find("Draw Text").gameObject;
        drawText.SetActive(true);
        Time.timeScale = 0f;
        DestroyAllBeats();
    }

    private IEnumerator AttackTargetSpawn()
    {
        yield return new WaitForSecondsRealtime(1f);
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