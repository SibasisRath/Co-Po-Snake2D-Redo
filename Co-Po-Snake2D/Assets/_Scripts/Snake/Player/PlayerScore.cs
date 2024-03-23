using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    private const int scoreMultipler = 1;
    private const int scoreMultiplerWithPowerUp = 2;
    private int score;
    private TextMeshProUGUI playerScoreText;

    private bool scoreBoostPowerUpIsActivated;

    public int Score { get => score; }
    public TextMeshProUGUI PlayerScoreText { get => playerScoreText; set => playerScoreText = value; }
    public bool ScoreBoostPowerUpIsActivated { get => scoreBoostPowerUpIsActivated; set => scoreBoostPowerUpIsActivated = value; }

    private void Start()
    {
        score = 0;
        UpdateUI();
    }

    public void UpdateScore(int additionalScore)
    {
        //This is because if the score boost power up is activated it will only increase the score value 2x for mass gainer food
        //according to the instruction.
        if (ScoreBoostPowerUpIsActivated && additionalScore > 0)
        {
             additionalScore *= scoreMultiplerWithPowerUp;
        }
        else
        {
            additionalScore *= scoreMultipler;
        }
        score += additionalScore;
        UpdateUI();
        Debug.Log(Score);
    }

    private void UpdateUI()
    {
        PlayerScoreText.text = "Player Score\n" + score;
    }
}
