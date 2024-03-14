using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    private int score;
    [SerializeField] private TextMeshProUGUI playerScore;

    public int Score { get => score; }

    private void Start()
    {
        score = 0;
        UpdateUI();
    }

    public void UpdateScore(int score)
    {
        this.score += score;
        UpdateUI();
        Debug.Log(Score);
    }

    private void UpdateUI()
    {
        playerScore.text = "Player Score\n" + score;
    }
}
