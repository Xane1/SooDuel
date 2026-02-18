using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    
    public Text scoreText;
    
   public int score = 0;

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        scoreText.text = score.ToString() + " Points";
    }

    // Update is called once per frame
    public void AddPoints()
    {
        score += 100;
        scoreText.text = score.ToString() + " Points";
    }

    public int GetScore()
    {
        return score;
    }
}
