using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    private const int SCORE_MULTIPLIER = 1;
    private const int SCORE_MULTIPLIER_WITH_POWER_UP = 2;
    private const string PLAYER_SCORE_UI_TEXT = "Player Score\n";
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
             additionalScore *= SCORE_MULTIPLIER_WITH_POWER_UP;
        }
        else
        {
            additionalScore *= SCORE_MULTIPLIER;
        }
        score += additionalScore;
        UpdateUI();
    }

    private void UpdateUI()
    {
        PlayerScoreText.text = PLAYER_SCORE_UI_TEXT + score;
    }
}
