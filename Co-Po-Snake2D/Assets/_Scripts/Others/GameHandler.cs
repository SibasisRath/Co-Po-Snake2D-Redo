using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private static GameHandler instance;
    private static GameModes mode;

    private int playerOneSize = 0;
    private int playerTwoSize = 0;
    private int currentMinimumSize = 0;

    private static (bool, PlayerEnum) gameResult; //Here the boolean represents the is it a suicide or not and the player enum represents who did it

    public static GameModes Mode { get => mode; set => mode = value; }
    public static (bool, PlayerEnum) GameResult { get => gameResult; set => gameResult = value; }
    public static GameHandler Instance { get => instance; set => instance = value; }
    public int CurrentMinimumSize { get => currentMinimumSize; private set => currentMinimumSize = value; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateMinimumSnakeSize(int size, PlayerEnum player)
    {
        AssigningPlayerSizes(size, player);

        switch (mode)
        {
            case GameModes.SinglePlayer:
                CurrentMinimumSize = playerOneSize;
                break;
            case GameModes.CopoPlayer:
                GettingMinimumSnakeSize();
                break;
            default:
                break;
        }
    }

    private void GettingMinimumSnakeSize()
    {
        if (playerOneSize > playerTwoSize)
        {
            CurrentMinimumSize = playerTwoSize;
        }
        else if (playerOneSize < playerTwoSize)
        {
            CurrentMinimumSize = playerOneSize;
        }
        else
        {
            CurrentMinimumSize = playerOneSize;
        }
    }

    private void AssigningPlayerSizes(int size, PlayerEnum player)
    {
        if (player == PlayerEnum.Player1)
        {
            playerOneSize = size;
        }
        else if (player == PlayerEnum.Player2)
        {
            playerTwoSize = size;
        }
    }
}
