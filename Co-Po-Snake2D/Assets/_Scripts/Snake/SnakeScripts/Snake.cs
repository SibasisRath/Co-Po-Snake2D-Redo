using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private PlayerEnum player;

    [SerializeField] private Timer timer; // Reference to timer
    [SerializeField] private Vector2Int snakeGridPosition; //Snake position on grid
    [SerializeField] private int gridUnit; // This is the length snake will move each time
    private Vector2Int gridMoveDirection; // This will help to change direction
    private Directions direction; // this will be the all the allowed direction
    private LevelGrid levelGrid; // Reference to Grid. This is to interact with consumable
    [SerializeField] private PlayerScore playerScore;

    private FoodScript foodScript;
    private GameObject powerUp;

    public void LevelGridSetUp(LevelGrid levelGrid){this.levelGrid = levelGrid;}

    private int snakeBodySize;
    private List<Vector2Int> snakeMovePositionLIst; // store the positions of the snake body parts
    private List<SnakeBodyPart> snakeBodyPartsList;

    private SnakeStates snakeState;

    //powerups activation variables
    private bool canDie;
    private int speedMultiplier = 1;
    private int foodScoreMultplier = 1;
    float defaultSpeed;


    public Directions Direction { get => direction; set => direction = value; }
    public int SnakeBodySize { get => snakeBodySize;}
    public SnakeStates SnakeState { get => snakeState; set => snakeState = value; }
    public bool CanDie { get => canDie; set => canDie = value; }
    public PlayerEnum Player { get => player;}

    private void Awake()
    {
        gridMoveDirection = new Vector2Int(0, gridUnit); //initial movement
        snakeBodySize = 0;
        snakeMovePositionLIst = new List<Vector2Int>();
        snakeBodyPartsList = new List<SnakeBodyPart>();
        defaultSpeed = timer.Speed;
    }

    private void Start()
    {
        SnakeState = SnakeStates.Alive;
        CanDie = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((GameHandler.State == GameStates.Start || GameHandler.State == GameStates.Resume) && SnakeState == SnakeStates.Alive)
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
        if (timer.CanPerform)
        {
            timer.CanPerform = false;
            snakeMovePositionLIst.Insert(0, snakeGridPosition);  //This will keep adding snakeHead's new grid postions at the beginning.
            snakeGridPosition += gridMoveDirection; // this is for changing direction.

            snakeGridPosition = levelGrid.ValidateGridPosition(snakeGridPosition); //This is a part of the screen wrapping feature.

            bool snakeAteFood = levelGrid.CheckSnakeAteFood(snakeGridPosition, out foodScript);

            if (snakeAteFood)
            {
                snakeBodySize += foodScript.BodyGrow;
                playerScore.UpdateScore(foodScript.Score * foodScoreMultplier);
                CreateSnakeBody(foodScript.BodyGrow);
                Debug.Log(SnakeBodySize);
            }

            bool snakeAtePowerUp = levelGrid.CheckSnakeAtePowerUp(snakeGridPosition, out powerUp);
            if (snakeAtePowerUp)
            {
                StartCoroutine(PowerUpTime(powerUp));
            }
            if (snakeMovePositionLIst.Count >= SnakeBodySize + 10)
            {
                snakeMovePositionLIst.RemoveAt(snakeMovePositionLIst.Count - 1);
            }

            if (CanDie)
            {
                (bool, bool) deathVarification = levelGrid.SnakeDeathCheck(snakeGridPosition, player);
                if (deathVarification.Item1)
                {
                    SnakeState = SnakeStates.Dead;
                    Debug.Log($"{player} is dead.");
                    GameHandler.GameResult = (deathVarification.Item2, player);
                    GameHandler.State = GameStates.GameOver;
                }
            }
        }
        transform.position = new Vector3(snakeGridPosition.x, snakeGridPosition.y);
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
                snakeBodyPartsList.Add(new SnakeBodyPart(snakeBodyPartsList.Count, player));
            }
            else
            {
                snakeBodyPartsList[snakeBodyPartsList.Count - 1].DestroyGameObject();           
                snakeBodyPartsList.RemoveAt(snakeBodyPartsList.Count -1);
            }   
        }   
    }

    private void UpdateSnakeBody()
    {
        for (int i = 0; i < snakeBodyPartsList.Count; i++)
        {
            snakeBodyPartsList[i].SetGridPosition(snakeMovePositionLIst[i]);
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

    public Vector2Int GetSnakeGritPosition(){return snakeGridPosition;}

    public List<Vector2Int> GetSnakePositions()
    {
        List<Vector2Int> entireSnake = new List<Vector2Int>() { snakeGridPosition };
        entireSnake.AddRange(snakeMovePositionLIst);
        return entireSnake;
    }

    public List<Vector2Int> GetSnakeBodyPositions()
    {
        List<Vector2Int> entireSnake = new List<Vector2Int>();
        entireSnake.AddRange(snakeBodyPartsList.Select<SnakeBodyPart, Vector2Int>(go => go.GetGridPosition()).ToList());
        return entireSnake;
    }

    private IEnumerator PowerUpTime(GameObject powerUp)
    {
        Debug.Log("entered power Up coroutine");
        GameObject activePowerUp = powerUp;
        PowerUpScript powerScript = activePowerUp.GetComponent<PowerUpScript>();
        InGameSprites powersprite = powerScript.PowerUpSprite;

        switch (powersprite)
        {
            case InGameSprites.ScoreBoostPowerUp:
                foodScoreMultplier = 2;
                break;
            case InGameSprites.ShieldPowerUp:
                CanDie = false;
                break;
            case InGameSprites.SpeedBoostPowerUp:
                speedMultiplier = 2;
                timer.Speed *= speedMultiplier;
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(powerScript.EffectiveTime);
        foodScoreMultplier = 1;
        CanDie = true;
        timer.Speed = defaultSpeed;
        
    }
}
