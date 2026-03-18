using UnityEngine;
using System.Collections;
using GameControllerScripts;
using UnityEngine.InputSystem;
using System.Linq;
public class CoOpMultiplayerGameManager : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    public RhythmAudioScript RhythmAudioScript;
    
    public GameObject MusicPlayer;
    public GameObject MusicBeatPlayer;
    public GameObject BeatSpawner;

    public GameObject PlayerMessage;
    public GameObject Tutorial;
    public GameObject P1ScoreKeeper;
    public GameObject P2ScoreKeeper;
    
    public int easyWinScore = 7000;
    public int normalWinScore = 12999;
    public int hardWinScore = 18000;

    bool gameEnded = false;
    
    
    bool activated = false;
    bool activated2 = false;

    // Update is called once per frame
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
        
        if (DifficultyManager.Instance != null)
        {
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Easy)
            {
                if (ScoreManager.instance.P1score + ScoreManager.instance.P2score >= easyWinScore && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
                {
                    WinGame();
                }
                else if (ScoreManager.instance.P1score + ScoreManager.instance.P2score< easyWinScore && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
                {
            
                    LoseGame();
                }
            }
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Normal)
            {
                if (ScoreManager.instance.P1score + ScoreManager.instance.P2score >= normalWinScore && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
                {
                    WinGame();
                }
                else if (ScoreManager.instance.P1score + ScoreManager.instance.P2score < normalWinScore && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
                {
            
                    LoseGame();
                }
            }
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Hard)
            {
                if (ScoreManager.instance.P1score + ScoreManager.instance.P2score >= hardWinScore && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
                {
                    WinGame();
                }
                else if (ScoreManager.instance.P1score + ScoreManager.instance.P2score< hardWinScore && RhythmAudioScript.songPosition >= RhythmAudioScript.musicSource.clip.length - RhythmAudioScript.endBeatOffset)
                {
            
                    LoseGame();
                }
            }
        }
    }
    private IEnumerator HandleLoseGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        GameOverScreen.Setup(ScoreManager.instance.P1score + ScoreManager.instance.P2score);
        GameObject lossText = GameObject.Find("GameOverBackground").transform.Find("Loss Text").gameObject;
        lossText.SetActive(true);
        Time.timeScale = 0f;
        DestroyAllBeats();
    }
    
    private IEnumerator HandleWinGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        GameOverScreen.Setup(ScoreManager.instance.P1score + ScoreManager.instance.P2score);
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
