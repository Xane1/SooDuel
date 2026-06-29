using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleThemeScript : MonoBehaviour
{
    public static TitleThemeScript Instance;
    private AudioSource audioSource;

    [SerializeField] public AudioClip titleTheme;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = titleTheme;
        audioSource.Play();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "VersusScene" || scene.name == "SingleplayerUscene" || 
            scene.name == "CoOpScene")
        {
            audioSource.Stop();
        }
        else
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}