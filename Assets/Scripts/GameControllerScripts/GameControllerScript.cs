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

    public GameObject TutorialScreen;
    
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
    public int hardWinScore = 15000;

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
        TutorialScreen.SetActive(false);
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
            
            //Present
            
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Easy  &&
                MapManager.Instance.CurrentStage == MapManager.Stage.Present)
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

            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Normal &&
                MapManager.Instance.CurrentStage == MapManager.Stage.Present)
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
            
            
            //Medieval
            
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Easy  &&
                MapManager.Instance.CurrentStage == MapManager.Stage.Medieval)
            {
                if (ScoreManager.instance.P1score >= easyWinScore &&  MedievalRhythmAudioScriptAlt.songPosition >=
                    MedievalRhythmAudioScriptAlt.musicSource.clip.length)
                {
                    WinGame();
                }
                else if (ScoreManager.instance.P1score < easyWinScore &&  MedievalRhythmAudioScriptAlt.songPosition >=
                         MedievalRhythmAudioScriptAlt.musicSource.clip.length)
                {

                    LoseGame();
                }
            }
            
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Normal &&
                MapManager.Instance.CurrentStage == MapManager.Stage.Medieval)
            {
                if (ScoreManager.instance.P1score >= normalWinScore && MedievalRhythmAudioScriptAlt.songPosition >=
                    MedievalRhythmAudioScriptAlt.musicSource.clip.length)
                {
                    WinGame();
                }
                else if (ScoreManager.instance.P1score < normalWinScore && MedievalRhythmAudioScriptAlt.songPosition >=
                         MedievalRhythmAudioScriptAlt.musicSource.clip.length)
                {

                    LoseGame();
                }
            }
            
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Hard &&
                MapManager.Instance.CurrentStage == MapManager.Stage.Medieval)
            {
                if (ScoreManager.instance.P1score >= hardWinScore && MedievalRhythmAudioScriptAlt.songPosition >=
                    MedievalRhythmAudioScriptAlt.musicSource.clip.length)
                {
                    WinGame();
                }
                else if (ScoreManager.instance.P1score < hardWinScore && MedievalRhythmAudioScriptAlt.songPosition >=
                         MedievalRhythmAudioScriptAlt.musicSource.clip.length)
                {

                    LoseGame();
                }
            }
            
            
            //Ancient 
            
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Easy  &&
                MapManager.Instance.CurrentStage == MapManager.Stage.Ancient)
            {
                if (ScoreManager.instance.P1score >= easyWinScore && AncientRhythmAudioScriptAlt.songPosition >=
                    AncientRhythmAudioScriptAlt.musicSource.clip.length)
                {
                    WinGame();
                }
                else if (ScoreManager.instance.P1score < easyWinScore && AncientRhythmAudioScriptAlt.songPosition >=
                         AncientRhythmAudioScriptAlt.musicSource.clip.length)
                {

                    LoseGame();
                }
            }
            
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Normal &&
                MapManager.Instance.CurrentStage == MapManager.Stage.Ancient)
            {
                if (ScoreManager.instance.P1score >= normalWinScore && AncientRhythmAudioScriptAlt.songPosition >=
                    AncientRhythmAudioScriptAlt.musicSource.clip.length)
                {
                    WinGame();
                }
                else if (ScoreManager.instance.P1score < normalWinScore && AncientRhythmAudioScriptAlt.songPosition >=
                         AncientRhythmAudioScriptAlt.musicSource.clip.length)
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