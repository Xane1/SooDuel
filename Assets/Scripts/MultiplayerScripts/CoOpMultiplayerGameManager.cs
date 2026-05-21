using UnityEngine;
using System.Collections;
using GameControllerScripts;
using UnityEngine.InputSystem;
using System.Linq;
public class CoOpMultiplayerGameManager : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    public RhythmAudioScriptAlt RhythmAudioScriptAlt;
    public RhythmAudioScriptAlt AncientRhythmAudioScriptAlt;
    public RhythmAudioScriptAlt MedievalRhythmAudioScriptAlt;
    
    public GameObject easyScoreGoal;
    public GameObject mediumScoreGoal;
    public GameObject hardScoreGoal;
    
    public GameObject AncientMusic;
    public GameObject MedievalMusic;
    public GameObject PresentMusic;
    
    public GameObject BeatSpawner;

    public GameObject Soo;
    public GameObject AncientSoo;

    
    public GameObject Tutorial;
    public GameObject PlayerMessage;
    public GameObject ScoreKeeper;
  //  public GameObject P2ScoreKeeper;
    
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
            ScoreKeeper.SetActive(true);
            activated = true;
            Tutorial.SetActive(false);
            PlayerMessage.SetActive(true);
            FindObjectOfType<MultiplayerManager>().EnableJoining();
        }
        if ((!activated2 && GameObject.FindGameObjectsWithTag("Player2").Length > 0) && MapManager.Instance.CurrentStage == MapManager.Stage.Present)
        {
           PresentMusic.SetActive(true);
            BeatSpawner.SetActive(true);
          //  P2ScoreKeeper.SetActive(true);
            PlayerMessage.SetActive(false);

            activated2 = true;
        }
        else if ((!activated2 && GameObject.FindGameObjectsWithTag("Player2").Length > 0) && MapManager.Instance.CurrentStage == MapManager.Stage.Medieval)
        {
            MedievalMusic.SetActive(true);
            BeatSpawner.SetActive(true);
          //  P2ScoreKeeper.SetActive(true);
            PlayerMessage.SetActive(false);

            activated2 = true;
        }
        else if ((!activated2 && GameObject.FindGameObjectsWithTag("Player2").Length > 0) && MapManager.Instance.CurrentStage == MapManager.Stage.Ancient)
        {
            AncientMusic.SetActive(true);
            BeatSpawner.SetActive(true);
        //    P2ScoreKeeper.SetActive(true);
            PlayerMessage.SetActive(false);

            activated2 = true;
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
        
        if (DifficultyManager.Instance != null && MapManager.Instance !=null)
        {
            
            //Present
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Easy   &&
                MapManager.Instance.CurrentStage == MapManager.Stage.Present)
            {
                easyScoreGoal.SetActive(true);
                
                if (ScoreManager.instance.P1score + ScoreManager.instance.P2score >= easyWinScore&& RhythmAudioScriptAlt.songPosition >= RhythmAudioScriptAlt.musicSource.clip.length)
                {
                    Soo.SetActive(true);
                    WinGame();
                }
                else if (ScoreManager.instance.P1score + ScoreManager.instance.P2score< easyWinScore && RhythmAudioScriptAlt.songPosition >= RhythmAudioScriptAlt.musicSource.clip.length)
                {
                    Soo.SetActive(true);
                    LoseGame();
                }
            }
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Normal &&
                MapManager.Instance.CurrentStage == MapManager.Stage.Present)
            {
                mediumScoreGoal.SetActive(true);
                
                if (ScoreManager.instance.P1score + ScoreManager.instance.P2score >= normalWinScore && RhythmAudioScriptAlt.songPosition >= RhythmAudioScriptAlt.musicSource.clip.length)
                {
                    Soo.SetActive(true);
                    WinGame();
                }
                else if (ScoreManager.instance.P1score + ScoreManager.instance.P2score < normalWinScore && RhythmAudioScriptAlt.songPosition >= RhythmAudioScriptAlt.musicSource.clip.length)
                {
                    Soo.SetActive(true);
                    LoseGame();
                }
            }
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Hard &&
                MapManager.Instance.CurrentStage == MapManager.Stage.Present)
            {
                hardScoreGoal.SetActive(true);

                if (ScoreManager.instance.P1score + ScoreManager.instance.P2score >= hardWinScore && RhythmAudioScriptAlt.songPosition >= RhythmAudioScriptAlt.musicSource.clip.length)
                {
                    Soo.SetActive(true);
                    WinGame();
                }
                else if (ScoreManager.instance.P1score + ScoreManager.instance.P2score< hardWinScore && RhythmAudioScriptAlt.songPosition >= RhythmAudioScriptAlt.musicSource.clip.length)
                {
                    Soo.SetActive(true);
                    LoseGame();
                }
            }
            
               //Medieval
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Easy   &&
                MapManager.Instance.CurrentStage == MapManager.Stage.Medieval)
            {
                easyScoreGoal.SetActive(true);

                if (ScoreManager.instance.P1score + ScoreManager.instance.P2score >= easyWinScore&& MedievalRhythmAudioScriptAlt.songPosition >= MedievalRhythmAudioScriptAlt.musicSource.clip.length)
                {
                    Soo.SetActive(true);
                    WinGame();
                }
                else if (ScoreManager.instance.P1score + ScoreManager.instance.P2score< easyWinScore && MedievalRhythmAudioScriptAlt.songPosition >= MedievalRhythmAudioScriptAlt.musicSource.clip.length)
                {
            
                    Soo.SetActive(true);
                    LoseGame();
                }
            }
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Normal &&
                MapManager.Instance.CurrentStage == MapManager.Stage.Medieval)
            {
                mediumScoreGoal.SetActive(true);

                if (ScoreManager.instance.P1score + ScoreManager.instance.P2score >= normalWinScore && MedievalRhythmAudioScriptAlt.songPosition >= MedievalRhythmAudioScriptAlt.musicSource.clip.length)
                {
                    Soo.SetActive(true);
                    WinGame();
                }
                else if (ScoreManager.instance.P1score + ScoreManager.instance.P2score < normalWinScore && MedievalRhythmAudioScriptAlt.songPosition >= MedievalRhythmAudioScriptAlt.musicSource.clip.length)
                {
            
                    Soo.SetActive(true);
                    LoseGame();
                }
            }
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Hard &&
                MapManager.Instance.CurrentStage == MapManager.Stage.Medieval)
            {
                hardScoreGoal.SetActive(true);

                if (ScoreManager.instance.P1score + ScoreManager.instance.P2score >= hardWinScore && MedievalRhythmAudioScriptAlt.songPosition >= MedievalRhythmAudioScriptAlt.musicSource.clip.length)
                {
                    Soo.SetActive(true);
                    WinGame();
                }
                else if (ScoreManager.instance.P1score + ScoreManager.instance.P2score< hardWinScore && MedievalRhythmAudioScriptAlt.songPosition >= MedievalRhythmAudioScriptAlt.musicSource.clip.length)
                {
                    Soo.SetActive(true);
                    LoseGame();
                }
            }
            
                 //Ancient
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Easy   &&
                MapManager.Instance.CurrentStage == MapManager.Stage.Ancient)
            {
                easyScoreGoal.SetActive(true);

                if (ScoreManager.instance.P1score + ScoreManager.instance.P2score >= easyWinScore&& AncientRhythmAudioScriptAlt.songPosition >= AncientRhythmAudioScriptAlt.musicSource.clip.length)
                {
                    AncientSoo.SetActive(true);
                    WinGame();
                }
                else if (ScoreManager.instance.P1score + ScoreManager.instance.P2score< easyWinScore && AncientRhythmAudioScriptAlt.songPosition >= AncientRhythmAudioScriptAlt.musicSource.clip.length)
                {
                    AncientSoo.SetActive(true);
                    LoseGame();
                }
            }
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Normal &&
                MapManager.Instance.CurrentStage == MapManager.Stage.Ancient)
            {
                mediumScoreGoal.SetActive(true);

                if (ScoreManager.instance.P1score + ScoreManager.instance.P2score >= normalWinScore && AncientRhythmAudioScriptAlt.songPosition >= AncientRhythmAudioScriptAlt.musicSource.clip.length)
                {
                    AncientSoo.SetActive(true);
                    WinGame();
                }
                else if (ScoreManager.instance.P1score + ScoreManager.instance.P2score < normalWinScore && AncientRhythmAudioScriptAlt.songPosition >= AncientRhythmAudioScriptAlt.musicSource.clip.length)
                {
            
                    AncientSoo.SetActive(true);
                    LoseGame();
                }
            }
            if (DifficultyManager.Instance.CurrentDifficulty == DifficultyManager.Difficulty.Hard &&
                MapManager.Instance.CurrentStage == MapManager.Stage.Ancient)
            {
                hardScoreGoal.SetActive(true);
                
                if (ScoreManager.instance.P1score + ScoreManager.instance.P2score >= hardWinScore && AncientRhythmAudioScriptAlt.songPosition >= AncientRhythmAudioScriptAlt.musicSource.clip.length)
                {
                    AncientSoo.SetActive(true);
                    WinGame();
                }
                else if (ScoreManager.instance.P1score + ScoreManager.instance.P2score< hardWinScore && AncientRhythmAudioScriptAlt.songPosition >= AncientRhythmAudioScriptAlt.musicSource.clip.length)
                {
                    AncientSoo.SetActive(true);
                    LoseGame();
                }
            }
        }
    }
    private IEnumerator HandleLoseGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        PauseManager.instance.DisableAllCursors(); 
        GameOverScreen.Setup(ScoreManager.instance.P1score + ScoreManager.instance.P2score);
        GameObject lossText = GameObject.Find("GameOverBackground").transform.Find("Loss Text").gameObject;
        lossText.SetActive(true);
        Time.timeScale = 0f;
        DestroyAllBeats();
    }
    
    private IEnumerator HandleWinGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        PauseManager.instance.DisableAllCursors(); 
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
