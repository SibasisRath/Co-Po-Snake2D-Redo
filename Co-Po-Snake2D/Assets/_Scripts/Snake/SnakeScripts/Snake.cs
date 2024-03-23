using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private PlayerEnum player;
    [SerializeField] private InGameSprites snakeBody;

    [SerializeField] private SnakeTimer timer; // Reference to timer
    [SerializeField] private Vector2Int snakeGridPosition; //Snake position on grid
    [SerializeField] private int gridUnit; // This is the length snake will move each time
    private Vector2Int gridMoveDirection; // This will help to change direction
    private Directions direction; // this will be the all the allowed direction

    [SerializeField] private int gridShift = 195;

    private int snakeBodySize;
    private int additionalSnakeBodySize;
    private List<Vector2Int> snakeMovePositionLIst; // store the positions of the snake body parts
    private List<SnakeBodyPart> snakeBodyPartsList;

    

    public Directions Direction { get => direction; set => direction = value; }
    public PlayerEnum Player { get => player;}
    public InGameSprites SnakeBody { get => snakeBody; }
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
    public int AdditionalSnakeBodySize 
    {
        get => additionalSnakeBodySize;
        set
        {
            additionalSnakeBodySize = value;
            SnakeBodySize += additionalSnakeBodySize;
            CreateSnakeBody(additionalSnakeBodySize);
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
            HandelDirection();
            HandleMovement();
        }
    }

    private void HandelDirection()
    {
        if (direction == Directions.Up && gridMoveDirection.y != -gridUnit)
        {
            gridMoveDirection.y = gridUnit;
            gridMoveDirection.x = 0;
        }
        else if (direction == Directions.Down && gridMoveDirection.y != gridUnit)
        {
            gridMoveDirection.y = -gridUnit;
            gridMoveDirection.x = 0;
        }
        else if (direction == Directions.Left && gridMoveDirection.x != gridUnit)
        {
            gridMoveDirection.x = -gridUnit;
            gridMoveDirection.y = 0;
        }
        else if (direction == Directions.Right && gridMoveDirection.x != -gridUnit)
        {
            gridMoveDirection.x = gridUnit;
            gridMoveDirection.y = 0;
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
        UpdateSnakeBody();
    }

    private void CreateSnakeBody(int extraBodyParts)
    {
        bool isGrowing = (extraBodyParts > 0) ? true : false;

        for (int i = 0; i < Mathf.Abs(extraBodyParts); i++)
        {
            if (isGrowing)
            {
                SnakeBodyPartsList.Add(new SnakeBodyPart(SnakeBodyPartsList.Count, this));
            }
            else
            {
                SnakeBodyPartsList[SnakeBodyPartsList.Count - 1].DestroyGameObject();           
                SnakeBodyPartsList.RemoveAt(SnakeBodyPartsList.Count -1);
            }   
        }   
    }

    private void UpdateSnakeBody()
    {
        for (int i = 0; i < SnakeBodyPartsList.Count; i++)
        {
            SnakeBodyPartsList[i].SetGridPosition(snakeMovePositionLIst[i]);
        }
    }

    private float HandleRotation(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        if (n<0)
        {
            n += 360;
        }
        return n;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"collision happen with : {collision.gameObject.name}");
        ScreenWrapping(collision);
    }

    private void ScreenWrapping(Collider2D collision)
    {
        //Snake Screen Wrapping feature
        if (collision.gameObject.CompareTag("RightBorder"))
        {
            snakeGridPosition.x -= gridShift;
        }
        else if (collision.gameObject.CompareTag("UpBorder"))
        {
            snakeGridPosition.y -= gridShift;
        }
        else if (collision.gameObject.CompareTag("LeftBorder"))
        {
            snakeGridPosition.x += gridShift;
        }
        else if (collision.gameObject.CompareTag("DownBorder"))
        {
            snakeGridPosition.y += gridShift;
        }
        SnakeGridPosition = snakeGridPosition;
    }
}
