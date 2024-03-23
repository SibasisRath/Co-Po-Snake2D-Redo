using UnityEngine;

public class ConsumableSpawner : MonoBehaviour
{
    [SerializeField] private int spawnAreawidth; //it's localScale will be used as spawned area.
    [SerializeField] private int spawnAreaHeight;

    [SerializeField] private float powerUpSpawnInterval = 10f;
    [SerializeField] private float foodSpawnInterval = 5f;

    [SerializeField] private float powerUpTimeSinceLastSpawn = 0f;
    [SerializeField] private float foodTimeSinceLastSpawn = 0f;

    private GameAssetManager assetManager;
    private GameHandler gameHandler;

    private float checkRadius = 1.0f;

    private void Start()
    {
        assetManager = GameAssetManager.Instance;
        gameHandler = GameHandler.Instance;
    }

    private void Update()
    {
        if (GameStateManager.GameState == GameStates.Running)
        {
            // Update timer for spawning power-ups
            powerUpTimeSinceLastSpawn += Time.deltaTime;
            if (powerUpTimeSinceLastSpawn >= powerUpSpawnInterval && GameStateManager.GameState == GameStates.Running)
            {
                SpawnPowerUps();
                powerUpTimeSinceLastSpawn = 0f;
            }

            // Update timer for other spawning method
            foodTimeSinceLastSpawn += Time.deltaTime;
            if (foodTimeSinceLastSpawn >= foodSpawnInterval && GameStateManager.GameState == GameStates.Running)
            {
                SpawnFood();
                foodTimeSinceLastSpawn = 0f;
            }
        }
    }

    private void SpawnFood()
    {
        if (GameStateManager.GameState == GameStates.Running)
        {
            GameObject food = assetManager.GetFoodObject(gameHandler.CurrentMinimumSize);
            food.transform.SetParent(transform);
            food.transform.position = GenerateRandomPosition();
        }
    }

    private void SpawnPowerUps()
    {
        if (GameStateManager.GameState == GameStates.Running)
        {
            GameObject powerUp = assetManager.GetPowerUp();
            powerUp.transform.SetParent(transform);
            powerUp.transform.position = GenerateRandomPosition();
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        Vector3 randomPosition;
        bool positionIsValid;

        do
        {
            randomPosition = new Vector3(UnityEngine.Random.Range(0, spawnAreawidth), UnityEngine.Random.Range(0, spawnAreaHeight), 0);

            // Check if there are any colliders overlapping with the randomly generated position
            Collider2D[] colliders = Physics2D.OverlapCircleAll(randomPosition, checkRadius);

            positionIsValid = colliders.Length == 0;

        } while (!positionIsValid);

        return randomPosition;
    }

}
