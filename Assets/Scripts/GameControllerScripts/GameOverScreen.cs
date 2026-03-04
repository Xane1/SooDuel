using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameControllerScripts
{
    public class GameOverScreen : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public Text pointsText;

        public void Setup(int score)
        {
            gameObject.SetActive(true);
            pointsText.text = score.ToString();
        }

        public void RestartButton()
        {
            Time.timeScale = 1f;
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }

        public void TitleScreenButton()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
