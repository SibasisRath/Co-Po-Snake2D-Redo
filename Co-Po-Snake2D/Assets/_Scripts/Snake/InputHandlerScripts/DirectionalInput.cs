using UnityEngine;

public class DirectionalInput : MonoBehaviour
{
    private PlayerEnum player;
    [SerializeField] private Snake snake;

    [Header("Player 1 Input Keys")]
    [SerializeField] private KeyCode player1UpKey = KeyCode.W;
    [SerializeField] private KeyCode player1LeftKey = KeyCode.A;
    [SerializeField] private KeyCode player1DownKey = KeyCode.S;
    [SerializeField] private KeyCode player1RightKey = KeyCode.D;

    [Header("Player 2 Input Keys")]
    [SerializeField] private KeyCode player2UpKey = KeyCode.UpArrow;
    [SerializeField] private KeyCode player2LeftKey = KeyCode.LeftArrow;
    [SerializeField] private KeyCode player2DownKey = KeyCode.DownArrow;
    [SerializeField] private KeyCode player2RightKey = KeyCode.RightArrow;

    private void Start()
    {
        player = snake.Player;
    }
    // Update is called once per frame
    void Update()
    {
        DirectionInput();
    }

    private void DirectionInput()
    {
        switch (player)
        {
            case PlayerEnum.Player1:
                PlayerInputs(player1UpKey, player1LeftKey, player1DownKey, player1RightKey);
                break;
            case PlayerEnum.Player2:
                PlayerInputs(player2UpKey, player2LeftKey, player2DownKey, player2RightKey);
                break;
            default:
                break;

        }
    }

    private void PlayerInputs(KeyCode upKey, KeyCode leftKey, KeyCode downKey, KeyCode rightKey)
    {
        if (Input.GetKeyDown(upKey))
        {
            ChangeDirection(Directions.Up);
        }
        else if (Input.GetKeyDown(leftKey))
        {
            ChangeDirection(Directions.Left);
        }
        else if (Input.GetKeyDown(downKey))
        {
            ChangeDirection(Directions.Down);
        }
        else if (Input.GetKeyDown(rightKey))
        {
            ChangeDirection(Directions.Right);
        }
    }

    private void ChangeDirection(Directions d)
    {
        snake.Direction = d;
    }
}
