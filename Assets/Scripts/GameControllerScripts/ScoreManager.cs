using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    
    public Text player1ScoreText;
    
    public Text player2ScoreText;
    
    public Text totalScoreText;
    
    public int P1score = 0;
    
    public int P2score = 0;
    
    public int totalScore = 0;

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        player1ScoreText.text = "Player 1" + "\n" + P1score.ToString() +
                                " Points";
        player2ScoreText.text = "Player 2 " + "\n" + P2score.ToString() +
                                " Points";
    }

    // Update is called once per frame
    public void P1AddPoints(int points)
    {
       P1score += points;
        player1ScoreText.text = "Player 1" + "\n" + P1score.ToString() +
                               " Points";
    }
    public void P2AddPoints(int points)
    {
        P2score += points;
        player2ScoreText.text = "Player 2" + "\n" + P2score.ToString() +
                                " Points";
    }

    public void TotalAddPoints()
    {
        int totalScore = P1score + P2score;
        totalScoreText.text = "Total Score: " + totalScore.ToString();
    }
    
}
