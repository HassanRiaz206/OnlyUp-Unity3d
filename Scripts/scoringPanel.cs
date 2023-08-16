using UnityEngine;
using UnityEngine.UI;

public class ScoringPanel : MonoBehaviour
{
    public Text scoreText;
    public Text timerText;

    private int playerScore = 0;
    private int collectableScore = 1; // Score added when a collectible is collected
    private float timer = 0f;

    private void Start()
    {
        UpdateScoreText();
    }

    private void Update()
    {
        UpdateTimer();
    }

    public void AddScore(int points)
    {
        playerScore += points;
        UpdateScoreText();
    }

    public void CollectCollectible()
    {
        Debug.Log("Collectible collected!");
        AddScore(collectableScore);
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + playerScore.ToString();
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
        timerText.text = "Time: " + timer.ToString("F2");
    }
}
