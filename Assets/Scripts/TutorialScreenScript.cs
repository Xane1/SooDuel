using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class TutorialScreenScript : MonoBehaviour
{
    //video objects
    public GameObject tutorialOne;
    public GameObject tutorialAttack;
    public GameObject tutorialParry;
    
    //Controller Text objects
    public GameObject tutorialOneText;
    
    //Dueling Text Objects
    public GameObject tutorialAttackText;
    public GameObject tutorialParryText;

    //Mouse Text Objects
    public GameObject tutorialMouseText;
    
    //CoOp Text Objects
    public GameObject coOpText;

    public GameObject duelControllerDiagram;

    public GameObject controllerDiagram;
    
    public GameObject NextButtonObject;

    private int tutorialStep = 0;

    void Start()
    {
        string selected = PlayerPrefs.GetString("SelectedButton", "");

        if (selected == "Versus")
        { 
            tutorialOne.SetActive(true);

        tutorialAttack.SetActive(false);
        tutorialParry.SetActive(false);

        tutorialOneText.SetActive(true);
        tutorialAttackText.SetActive(false);
        tutorialParryText.SetActive(false);

        controllerDiagram.SetActive(false);
        
        duelControllerDiagram.SetActive(true);
        }
        
        if (selected == "Controller" || selected == "CoOp")
        { 
            tutorialOne.SetActive(true);

            tutorialAttack.SetActive(false);
            tutorialParry.SetActive(false);

            tutorialOneText.SetActive(true);
            tutorialAttackText.SetActive(false);
            tutorialParryText.SetActive(false);

            duelControllerDiagram.SetActive(false);
            controllerDiagram.SetActive(true);
            tutorialStep = 2;
        }

        if (selected == "Mouse")
        {
            tutorialOne.SetActive(true);

            tutorialAttack.SetActive(false);
            tutorialParry.SetActive(false);

            tutorialMouseText.SetActive(true);
            tutorialOneText.SetActive(false);
            tutorialAttackText.SetActive(false);
            tutorialParryText.SetActive(false);
            
            duelControllerDiagram.SetActive(false);
            controllerDiagram.SetActive(false);
            tutorialStep = 2;
        }
    }
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame || Gamepad.current?.buttonEast.wasPressedThisFrame == true)
        {
            GoBack();
        }
    }

    public void NextButton()
    {
        UISoundManager.Instance.playSound(UISoundManager.Instance.clickSound);
        string selected = PlayerPrefs.GetString("SelectedButton", "");
        tutorialStep++;
        
        //Versus tutorial
        if (tutorialStep == 1 && selected == "Versus" )
        {
                tutorialOne.SetActive(false);
                tutorialAttack.SetActive(true);

                tutorialOneText.SetActive(false);
                tutorialAttackText.SetActive(true);
        }
        else if (tutorialStep == 2 && selected == "Versus")
        {
                tutorialAttack.SetActive(false);
                tutorialParry.SetActive(true);

                tutorialAttackText.SetActive(false);
                tutorialParryText.SetActive(true); 
        }
        else if (tutorialStep == 3 && selected == "Versus") 
        {
                Time.timeScale = 1f;
                SceneManager.LoadScene("StageSelect");
                
        }
        
        //CoOp tutorial
        if (tutorialStep == 1 && selected == "CoOp" )
        {
            
        }
        else if (tutorialStep == 2 && selected == "CoOp")
        {
            
        }
        else if (tutorialStep == 3 && selected == "CoOp") 
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("StageSelect");
                
        }
        
        
        //Controller tutorial
        if (tutorialStep == 1 && selected == "Controller" || selected == "CoOp" )
        {
            
        }
        else if (tutorialStep == 2 && selected == "Controller" || selected == "CoOp")
        {
            
        }
        else if (tutorialStep == 3 && selected == "Controller" || selected == "CoOp") 
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("StageSelect");
                
        }
        
        //Mouse tutorial
        if (tutorialStep == 1 && selected == "Mouse" )
        {
            
        }
        else if (tutorialStep == 2 && selected == "Mouse")
        {
            
        }
        else if (tutorialStep == 3 && selected == "Mouse") 
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("StageSelect");
                
        }
    }


    public void SkipTutorial()
    {
        UISoundManager.Instance.playSound(UISoundManager.Instance.clickSound);
        Time.timeScale = 1f;
     //   PlayerPrefs.SetString("TargetScene", "MouseScene");
        SceneManager.LoadScene("StageSelect");
    }
    
    void GoBack()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
