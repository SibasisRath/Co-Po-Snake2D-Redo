using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private const string RIGHT_BORDER = "RightBorder";
    private const string UP_BORDER = "UpBorder";
    private const string LEFT_BORDER = "LeftBorder";
    private const string DOWN_BORDER = "DownBorder";

    [SerializeField] private PlayerEnum player;
    [SerializeField] private GameObject snakeBody;

    [SerializeField] private SnakeTimer timer; // Reference to timer
    [SerializeField] private Vector2Int snakeGridPosition; //Snake position on grid
    [SerializeField] private int gridUnit; // This is the length snake will move each time
    private Vector2Int gridMoveDirection; // This will help to change direction
    private Directions direction; // this will be the all the allowed direction

    [SerializeField] private int gridShift = 195;

    private int snakeBodySize;
    private List<Vector2Int> snakeMovePositionLIst; // store the positions of the snake body parts
    private List<SnakeBodyPart> snakeBodyPartsList;

    public Directions Direction { get => direction; set => direction = value; }
    public PlayerEnum Player { get => player;}
    public GameObject SnakeBody { get => snakeBody; }
    public Vector2Int SnakeGridPosition { get => snakeGridPosition; set => snakeGridPosition = value; }
    public int SnakeBodySize
    {
        get => snakeBodySize;
        private set
        {
            snakeBodySize = value;
            GameHandler.Instance.UpdateMinimumSnakeSize(snakeBodySize, player);
        }
    }

    public List<SnakeBodyPart> SnakeBodyPartsList { get => snakeBodyPartsList; set => snakeBodyPartsList = value; }

    private void Awake()
    {
        gridMoveDirection = new Vector2Int(0, gridUnit); //initial movement
        SnakeBodySize = 0;
        snakeMovePositionLIst = new List<Vector2Int>();
        SnakeBodyPartsList = new List<SnakeBodyPart>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.GameState == GameStates.Running)
        {
            HandleDirection();
            HandleMovement();
        }
    }

    private void HandleDirection()
    {
        Vector2Int currentGridMoveDirection = gridMoveDirection;
        int newX = 0;
        int newY = 0;

        switch (direction)
        {
            case Directions.Up:
                newY = gridUnit;
                break;
            case Directions.Down:
                newY = -gridUnit;
                break;
            case Directions.Left:
                newX = -gridUnit;
                break;
            case Directions.Right:
                newX = gridUnit;
                break;
            default:
                break;
        }

        SetGridMoveDirection(newX, newY);
    }

    private void SetGridMoveDirection(int newX, int newY)
    {
        if (gridMoveDirection.x != newX || gridMoveDirection.y != newY)
        {
            gridMoveDirection.x = newX;
            gridMoveDirection.y = newY;
        }
    }

    private void HandleMovement()
    {
        if (timer.CanMove)
        {
            timer.CanMove = false;
            snakeMovePositionLIst.Insert(0, SnakeGridPosition);  //This will keep adding snakeHead's new grid postions at the beginning.
            SnakeGridPosition += gridMoveDirection; // this is for changing direction.
        }

        if (snakeMovePositionLIst.Count >= SnakeBodySize + 10)
        {
            snakeMovePositionLIst.RemoveAt(snakeMovePositionLIst.Count - 1);
        }

        transform.position = new Vector3(SnakeGridPosition.x, SnakeGridPosition.y);
        transform.eulerAngles = new Vector3(0,0,HandleRotation(gridMoveDirection) - 90f);
        UpdateSnakeBodyPartsPositions();
    }

    public void IncreaseBody(int numberOfBodyParts)
    {
        SnakeBodySize += numberOfBodyParts;

        for (int i = 0; i < Mathf.Abs(numberOfBodyParts); i++)
        {
            SnakeBodyPartsList.Add(new SnakeBodyPart(SnakeBodyPartsList.Count, this));
        }
    }

    public void DecreaseBody(int numberOfBodyParts)
    {
        snakeBodySize -= numberOfBodyParts;

        for (int i = 0; i < Mathf.Abs(numberOfBodyParts); i++)
        {
            SnakeBodyPartsList[SnakeBodyPartsList.Count - 1].DestroyGameObject();
            SnakeBodyPartsList.RemoveAt(SnakeBodyPartsList.Count - 1);
        }
    }

    private void UpdateSnakeBodyPartsPositions()
    {
        for (int i = 0; i < SnakeBodyPartsList.Count; i++)
        {
            SnakeBodyPartsList[i].SetGridPosition(snakeMovePositionLIst[i]);
        }
    }

    private float HandleRotation(Vector2Int dir)
    {
        const int rotationAdjustment = 360;
        float n = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        if (n<0)
        {
            n += rotationAdjustment;
        }
        return n;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScreenWrapping(collision);
    }

    private void ScreenWrapping(Collider2D collision)
    {
        //Snake Screen Wrapping feature
        if (collision.gameObject.CompareTag(RIGHT_BORDER))
        {
            snakeGridPosition.x -= gridShift;
        }
        else if (collision.gameObject.CompareTag(UP_BORDER))
        {
            snakeGridPosition.y -= gridShift;
        }
        else if (collision.gameObject.CompareTag(LEFT_BORDER))
        {
            snakeGridPosition.x += gridShift;
        }
        else if (collision.gameObject.CompareTag(DOWN_BORDER))
        {
            snakeGridPosition.y += gridShift;
        }
        SnakeGridPosition = snakeGridPosition;
    }
}
