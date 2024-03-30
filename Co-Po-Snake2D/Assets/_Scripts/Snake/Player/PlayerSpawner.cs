using TMPro;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Snake snakePlayer1Prefab;
    [SerializeField] private Snake snakePlayer2PreFab;

    private GameObject snakePlayer1;
    private GameObject snakePlayer2;

    [SerializeField] private TextMeshProUGUI scorePlayer1;
    [SerializeField] private TextMeshProUGUI scorePlayer2;

    private void Awake()
    {
        snakePlayer1 = Instantiate(snakePlayer1Prefab.gameObject);
        snakePlayer2 = Instantiate(snakePlayer2PreFab.gameObject);

        snakePlayer1.transform.SetParent(transform);
        snakePlayer2.transform.SetParent(transform);

        snakePlayer1.GetComponent<PlayerScore>().PlayerScoreText = scorePlayer1;
        snakePlayer2.GetComponent<PlayerScore>().PlayerScoreText = scorePlayer2;

        if (GameHandler.Mode == GameModes.SinglePlayer)
        {
            snakePlayer1.SetActive(true);
            snakePlayer2.SetActive(false);
        }
        else if (GameHandler.Mode == GameModes.CopoPlayer)
        {
            snakePlayer1.SetActive(true);
            snakePlayer2.SetActive(true);
        }
    }
}
