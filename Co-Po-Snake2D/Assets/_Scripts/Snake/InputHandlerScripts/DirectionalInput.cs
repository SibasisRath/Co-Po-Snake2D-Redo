using UnityEngine;

public class DirectionalInput : MonoBehaviour
{
    private const string HORIZONTAL_PLAYER_ONE = "HorizontalPlayer1";
    private const string VERTICAL_PLAYER_ONE = "VerticalPlayer1";
    private const string HORIZONTAL_PLAYER_TWO = "HorizontalPlayer2";
    private const string VERTICAL_PLAYER_TWO = "VerticalPlayer2";
    private PlayerEnum player;
    [SerializeField] private Snake snake;

    private string horizontalAxis;
    private string verticalAxis;

    private void Start()
    {
        player = snake.Player;
        // Set the horizontal and vertical axes based on the player
        if (player == PlayerEnum.Player1)
        {
            horizontalAxis = HORIZONTAL_PLAYER_ONE;
            verticalAxis = VERTICAL_PLAYER_ONE;
        }
        else if (player == PlayerEnum.Player2)
        {
            horizontalAxis = HORIZONTAL_PLAYER_TWO;
            verticalAxis = VERTICAL_PLAYER_TWO;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DirectionInput();
    }

    private void DirectionInput()
    {
        // Get the input from the respective axes
        float horizontalInput = Input.GetAxis(horizontalAxis);
        float verticalInput = Input.GetAxis(verticalAxis);

        // Determine the direction based on the input
        if (horizontalInput > 0)
        {
            ChangeDirection(Directions.Right);
        }
        else if (horizontalInput < 0)
        {
            ChangeDirection(Directions.Left);
        }
        else if (verticalInput > 0)
        {
            ChangeDirection(Directions.Up);
        }
        else if (verticalInput < 0)
        {
            ChangeDirection(Directions.Down) ;
        }
    }

    private void ChangeDirection(Directions newDirection)
    {
        snake.Direction = newDirection;
    }
}