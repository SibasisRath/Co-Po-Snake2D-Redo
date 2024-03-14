using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private const float minFoodSpawnInterval = 5f;
    private const float maxFoodSpawnInterval = 10f;
    private const float minPowerUpSpawnInterval = 7f;
    private const float maxPowerUpSpawnInterval = 10f;
    private static GameHandler instance;

    private static GameStates state;
    private static GameModes mode;
    [SerializeField] private GameModeManager modeManager;
    [SerializeField] private Snake snakeReferenceOne;
    [SerializeField] private Snake snakeReferenceTwo;
    private LevelGrid levelGrid;

    private static (bool, PlayerEnum) gameResult;

    public static GameStates State { get => state; set => state = value; }
    public static GameModes Mode { get => mode; private set => mode = value; }
    public static (bool, PlayerEnum) GameResult { get => gameResult; set => gameResult = value; }
    public static GameHandler Instance { get => instance; set => instance = value; }


    private void Awake()
    {
        Mode = modeManager.GameMode;
        Debug.Log(Mode);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game started");
        State = GameStates.Start;

        levelGrid = new LevelGrid(200,200, 10);

       

        snakeReferenceOne.LevelGridSetUp(levelGrid);
        levelGrid.SnakeSetUp(snakeReferenceOne);

        if (Mode == GameModes.SinglePlayer)
        {
            snakeReferenceTwo.gameObject.SetActive(false);
        }
        else if (Mode == GameModes.CopoPlayer)
        {
            snakeReferenceTwo.gameObject.SetActive(true);
            snakeReferenceTwo.LevelGridSetUp(levelGrid);
            levelGrid.SnakeSetUp(snakeReferenceTwo);
        }


        levelGrid.SpawnFood();
        levelGrid.SpawnPowerUps();
        StartCoroutine(FoodSpawnCoroutine());
        StartCoroutine(PowerUpSpawnCoroutine());
    }

    private IEnumerator FoodSpawnCoroutine()
    {
        while (true)
        {
            // Wait for a random interval before attempting to spawn food
            float randomInterval = Random.Range(minFoodSpawnInterval, maxFoodSpawnInterval);
            yield return new WaitForSeconds(randomInterval);

            // Check if the game is not paused before spawning food
            if (State != GameStates.Pause && State != GameStates.GameOver)
            {
                levelGrid.SpawnFood();
            }
        }
    }

    private IEnumerator PowerUpSpawnCoroutine()
    {
        while (true)
        {
            // Wait for a random interval before attempting to spawn food
            float randomInterval = Random.Range(minPowerUpSpawnInterval, maxPowerUpSpawnInterval);
            yield return new WaitForSeconds(randomInterval);

            // Check if the game is not paused before spawning food
            if (State != GameStates.Pause && State != GameStates.GameOver)
            {
                levelGrid.SpawnPowerUps();
            }
        }
    }

    private void Update()
    {

        if (State != GameStates.Pause & Input.GetKeyDown(KeyCode.P)) // visual instruction is given in the game.
        {
            State = GameStates.Pause;
        }

        levelGrid.DestroyFood();
    }
}
