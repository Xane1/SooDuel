using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleScreenScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
}
