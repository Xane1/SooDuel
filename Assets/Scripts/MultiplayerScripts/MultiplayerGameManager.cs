using UnityEngine;
using System.Collections;
using GameControllerScripts;
using UnityEngine.InputSystem;
using System.Linq;
public class MultiplayerGameManager : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    public RhythmAudioScriptVersus RhythmAudioScriptVersus;
    public RhythmAudioScriptVersus AncientRhythmAudioScriptVersus;
    public RhythmAudioScriptVersus MedievalRhythmAudioScriptVersus;

    public GameObject AncientMusic;
    public GameObject MedievalMusic;
    public GameObject PresentMusic;
    public GameObject BeatSpawner;
    
    public GameObject PlayerMessage;
 //   public GameObject Tutorial;
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
            P1ScoreKeeper.SetActive(true);
            FindObjectOfType<MultiplayerManager>().EnableJoining();
            activated = true;
        }
        if ((!activated2 && GameObject.FindGameObjectsWithTag("Player2").Length > 0) && MapManager.Instance.CurrentStage == MapManager.Stage.Present)
        {
           PresentMusic.SetActive(true);
            BeatSpawner.SetActive(true);
            P2ScoreKeeper.SetActive(true);
            PlayerMessage.SetActive(false);

            activated2 = true;
        }
        else if ((!activated2 && GameObject.FindGameObjectsWithTag("Player2").Length > 0) && MapManager.Instance.CurrentStage == MapManager.Stage.Medieval)
        {
            MedievalMusic.SetActive(true);
            BeatSpawner.SetActive(true);
            P2ScoreKeeper.SetActive(true);
            PlayerMessage.SetActive(false);

            activated2 = true;
        }
        else if ((!activated2 && GameObject.FindGameObjectsWithTag("Player2").Length > 0) && MapManager.Instance.CurrentStage == MapManager.Stage.Ancient)
        {
            AncientMusic.SetActive(true);
            BeatSpawner.SetActive(true);
            P2ScoreKeeper.SetActive(true);
            PlayerMessage.SetActive(false);

            activated2 = true;
        }

        if (gameEnded) return;
        
               if (MapManager.Instance.CurrentStage == MapManager.Stage.Ancient)
               {
                     if (AncientRhythmAudioScriptVersus == null || AncientRhythmAudioScriptVersus.musicSource == null || AncientRhythmAudioScriptVersus.musicSource.clip == null)
                         return;
               }
               else if (MapManager.Instance.CurrentStage == MapManager.Stage.Medieval) 
               {
                     if (MedievalRhythmAudioScriptVersus == null || MedievalRhythmAudioScriptVersus.musicSource == null || MedievalRhythmAudioScriptVersus.musicSource.clip == null)
                         return;
               }
               else if (MapManager.Instance.CurrentStage == MapManager.Stage.Present) 
               {
                     if (RhythmAudioScriptVersus == null || RhythmAudioScriptVersus.musicSource == null || RhythmAudioScriptVersus.musicSource.clip == null)
                         return; 
               }

        if (DifficultyManager.Instance != null && MapManager.Instance != null)
        {
            //Present
            if (MapManager.Instance.CurrentStage == MapManager.Stage.Present)
            {
                if (ScoreManager.instance.P1score > ScoreManager.instance.P2score &&
                    RhythmAudioScriptVersus.songPosition >= RhythmAudioScriptVersus.musicSource.clip.length)
                {
                    P1WinGame();
                }
                else if (ScoreManager.instance.P1score < ScoreManager.instance.P2score &&
                         RhythmAudioScriptVersus.songPosition >=
                         RhythmAudioScriptVersus.musicSource.clip.length)
                {
                    P2WinGame();
                }
                else if (ScoreManager.instance.P1score == ScoreManager.instance.P2score &&
                         RhythmAudioScriptVersus.songPosition >=
                         RhythmAudioScriptVersus.musicSource.clip.length)
                {
                    DrawGame();
                }
            }

            //Medieval
            if (MapManager.Instance.CurrentStage == MapManager.Stage.Medieval)
            {
                if (ScoreManager.instance.P1score > ScoreManager.instance.P2score &&
                    MedievalRhythmAudioScriptVersus.songPosition >=
                    MedievalRhythmAudioScriptVersus.musicSource.clip.length)
                {
                    P1WinGame();
                }
                else if (ScoreManager.instance.P1score < ScoreManager.instance.P2score &&
                         MedievalRhythmAudioScriptVersus.songPosition >=
                         MedievalRhythmAudioScriptVersus.musicSource.clip.length)
                {
                    P2WinGame();
                }
                else if (ScoreManager.instance.P1score == ScoreManager.instance.P2score &&
                         MedievalRhythmAudioScriptVersus.songPosition >=
                         MedievalRhythmAudioScriptVersus.musicSource.clip.length)
                {
                    DrawGame();
                }
            }

            //Ancient
            if (MapManager.Instance.CurrentStage == MapManager.Stage.Ancient)
            {
                if (ScoreManager.instance.P1score > ScoreManager.instance.P2score &&
                    AncientRhythmAudioScriptVersus.songPosition >=
                    AncientRhythmAudioScriptVersus.musicSource.clip.length)
                {
                    P1WinGame();
                }
                else if (ScoreManager.instance.P1score < ScoreManager.instance.P2score &&
                         AncientRhythmAudioScriptVersus.songPosition >=
                         AncientRhythmAudioScriptVersus.musicSource.clip.length)
                {
                    P2WinGame();
                }
                else if (ScoreManager.instance.P1score == ScoreManager.instance.P2score &&
                         AncientRhythmAudioScriptVersus.songPosition >=
                         AncientRhythmAudioScriptVersus.musicSource.clip.length)
                {
                    DrawGame();
                }
            }

        }
    }
    private IEnumerator HandleP2WinGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        PauseManager.instance.DisableAllCursors(); 
        GameOverScreen.Setup(ScoreManager.instance.P2score);
        GameObject p2WinText = GameObject.Find("GameOverBackground").transform.Find("Player2 WinText").gameObject;
        p2WinText.SetActive(true);
        Time.timeScale = 0f;
        DestroyAllBeats();
    }
    
    private IEnumerator HandleP1WinGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        PauseManager.instance.DisableAllCursors(); 
        GameOverScreen.Setup(ScoreManager.instance.P1score);
        GameObject p1WinText = GameObject.Find("GameOverBackground").transform.Find("Player1 WinText").gameObject;
        p1WinText.SetActive(true);
        Time.timeScale = 0f;
        DestroyAllBeats();
    }

    private IEnumerator HandleDrawGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        PauseManager.instance.DisableAllCursors(); 
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