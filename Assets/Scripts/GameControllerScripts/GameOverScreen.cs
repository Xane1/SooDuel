using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
namespace GameControllerScripts
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private GameObject firstSelectedButton;
        
        public Text pointsText;

        public void Setup(int score)
        {
            gameObject.SetActive(true);
            pointsText.text = score.ToString(); 
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        }

        public void RestartButton()
        {
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
            Time.timeScale = 1f;
        }

        public void TitleScreenButton()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
