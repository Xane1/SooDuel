using UnityEditor;
using UnityEngine;
using System.Collections;
using GameControllerScripts;
using UnityEngine.InputSystem;
using System.Linq;
public class GameManager : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    public RhythmAudioScriptAlt RhythmAudioScriptAlt;
    public RhythmAudioScriptAlt AncientRhythmAudioScriptAlt;
    public RhythmAudioScriptAlt MedievalRhythmAudioScriptAlt;

    
    public GameObject AncientMusic;
    public GameObject MedievalMusic;
    public GameObject PresentMusic;
    
   /* public GameObject MusicPlayer;
    public GameObject MusicBeatPlayer; */
    public GameObject BeatSpawner;
    
    public GameObject ReadyMessage;
    public GameObject ScoreKeeper;

    public int easyWinScore = 7000;
    public int normalWinScore = 12999;
    public int hardWinScore = 18000;

    bool gameEnded = false;
    
    bool activated = false;
   
    void ActivateGame()
    {
        StartCoroutine(ActivateAfterDelay());
    }

    IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);

        if (MapManager.Instance.CurrentStage == MapManager.Stage.Ancient)
        {
            AncientMusic.SetActive(true);
        }
        if (MapManager.Instance.CurrentStage == MapManager.Stage.Medieval)
        {
            MedievalMusic.SetActive(true);
        }
        if (MapManager.Instance.CurrentStage == MapManager.Stage.Present)
        {
           PresentMusic.SetActive(true);
        }
     /*   MusicPlayer.SetActive(true);
        MusicBeatPlayer.SetActive(true); */
        BeatSpawner.SetActive(true);
        ReadyMessage.SetActive(false);
        ScoreKeeper.SetActive(true);
        activated = true;
    }
    
    void Update()
    {
      //Tutorial Screen
         if ((!activated && Gamepad.all.Any(g => g.leftShoulder.wasPressedThisFrame)))
         {
            ActivateGame();
         }
         else if ((!activated && Pointer.current.press.wasPressedThisFrame)) 
         {
             ActivateGame(); 
         } 
         if (gameEnded) return;
         
         if (MapManager.Instance.CurrentStage == MapManager.Stage.Ancient)
         {
             if (AncientRhythmAudioScriptAlt == null || AncientRhythmAudioScriptAlt.musicSource == null || AncientRhythmAudioScriptAlt.musicSource.clip == null)
                 return;
         }
         else if (MapManager.Instance.CurrentStage == MapManager.Stage.Medieval)
         {
             if (MedievalRhythmAudioScriptAlt == null || MedievalRhythmAudioScriptAlt.musicSource == null || MedievalRhythmAudioScriptAlt.musicSource.clip == null)
                 return;
         }
         else if (MapManager.Instance.CurrentStage == MapManager.Stage.Present)
         {
             if (RhythmAudioScriptAlt == null || RhythmAudioScriptAlt.musicSource == null || RhythmAudioScriptAlt.musicSource.clip == null)
                 return;
         }

        //Win Conditions
        if (DifficultyManager.Instance != null && MapManager.Instance != null)
        {
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Easy)
            {
                if (ScoreManager.instance.P1score >= easyWinScore && RhythmAudioScriptAlt.songPosition >=
                    RhythmAudioScriptAlt.musicSource.clip.length)
                {
                    WinGame();
                }
                else if (ScoreManager.instance.P1score < easyWinScore && RhythmAudioScriptAlt.songPosition >=
                         RhythmAudioScriptAlt.musicSource.clip.length)
                {

                    LoseGame();
                }
            }

            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Normal)
            {
                if (ScoreManager.instance.P1score >= normalWinScore && RhythmAudioScriptAlt.songPosition >=
                    RhythmAudioScriptAlt.musicSource.clip.length)
                {
                    WinGame();
                }
                else if (ScoreManager.instance.P1score < normalWinScore && RhythmAudioScriptAlt.songPosition >=
                         RhythmAudioScriptAlt.musicSource.clip.length)
                {

                    LoseGame();
                }
            }

            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Hard &&
                MapManager.Instance.CurrentStage == MapManager.Stage.Ancient)
            {
                if (ScoreManager.instance.P1score >= hardWinScore && AncientRhythmAudioScriptAlt.songPosition >=
                  AncientRhythmAudioScriptAlt.musicSource.clip.length)
                {
                    WinGame();
                }
                else if (ScoreManager.instance.P1score < hardWinScore && AncientRhythmAudioScriptAlt.songPosition >=
                         AncientRhythmAudioScriptAlt.musicSource.clip.length)
                {

                    LoseGame();
                }
            }
            Debug.Log($"Ancient Hard check — songPosition: {AncientRhythmAudioScriptAlt.songPosition}, clip.length: {AncientRhythmAudioScriptAlt.musicSource?.clip?.length}, score: {ScoreManager.instance.P1score}");

            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Hard &&
                MapManager.Instance.CurrentStage == MapManager.Stage.Present)
            {
                if (ScoreManager.instance.P1score >= hardWinScore && RhythmAudioScriptAlt.songPosition >=
                    RhythmAudioScriptAlt.musicSource.clip.length)
                {
                    WinGame();
                }
                else if (ScoreManager.instance.P1score < hardWinScore && RhythmAudioScriptAlt.songPosition >=
                         RhythmAudioScriptAlt.musicSource.clip.length)
                {

                    LoseGame();
                }
            }
        }
    }
    private IEnumerator HandleLoseGame()
    {
        yield return new WaitForSecondsRealtime(1f);
    //    PauseManager.instance.DisableAllCursors(); 
        GameOverScreen.Setup(ScoreManager.instance.P1score);
        GameObject lossText = GameObject.Find("GameOverBackground").transform.Find("Loss Text").gameObject;
        lossText.SetActive(true);
        Time.timeScale = 0f;
        DestroyAllBeats();
    }
    
    private IEnumerator HandleWinGame()
    {
        yield return new WaitForSecondsRealtime(1f);
       // PauseManager.instance.DisableAllCursors(); 
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