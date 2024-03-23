using TMPro;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private GameObject snakePlayer1;
    private GameObject snakePlayer2;

    [SerializeField] private TextMeshProUGUI scorePlayer1;
    [SerializeField] private TextMeshProUGUI scorePlayer2;

    private void Awake()
    {
        snakePlayer1 = GameAssetManager.Instance.GetSnakeHeadPart(PlayerEnum.Player1);
        snakePlayer2 = GameAssetManager.Instance.GetSnakeHeadPart(PlayerEnum.Player2);

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
