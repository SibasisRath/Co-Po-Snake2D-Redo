using System.Collections;
using UnityEngine;

public class FoodSpawner : ConsumableSpawner
{
    private const int MINIMUM_SNAKE_BODY_LIMIT = 5;
    private const int TOTAL_MASS_GAINER_FOOD = 2;
    private const int TOTAL_NUMBER_OF_FOOD = 4;

    [SerializeField] private float foodSpawnInterval = 5f;

    //Prefabs
    [Header("Foods")]
    [SerializeField] private MassGainer massGainer1;
    [SerializeField] private MassGainer massGainer2;
    [SerializeField] private MassBurner massBurner1;
    [SerializeField] private MassBurner massBurner2;

    protected GameHandler gameHandler;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFoodCoroutine());
        gameHandler = GameHandler.Instance;
    }

    private IEnumerator SpawnFoodCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(foodSpawnInterval);
            if (GameStateManager.GameState == GameStates.Running)
                SpawnFood();
        }
    }

    private void SpawnFood()
    {
        GameObject food = GetFoodObject(gameHandler.CurrentMinimumSize);
        food.transform.SetParent(transform);
        food.transform.position = GenerateRandomPosition();
    }

    public GameObject GetFoodObject(int snakeSize)
    {
        GameObject foodObject;
        if (snakeSize < MINIMUM_SNAKE_BODY_LIMIT)
        {
            int a = Random.Range(0, TOTAL_MASS_GAINER_FOOD);
            foodObject = (a == 0) ? InstantiateFood(InGameSprites.MassGainer1) : InstantiateFood(InGameSprites.MassGainer2);
        }
        else
        {
            int a = Random.Range(0, TOTAL_NUMBER_OF_FOOD);
            switch (a)
            {
                case 0:
                    foodObject = InstantiateFood(InGameSprites.MassBurner1);
                    break;
                case 1:
                    foodObject = InstantiateFood(InGameSprites.MassBurner2);
                    break;
                case 2:
                    foodObject = InstantiateFood(InGameSprites.MassGainer1);
                    break;
                case 3:
                    foodObject = InstantiateFood(InGameSprites.MassGainer2);
                    break;
                default:
                    foodObject = null;
                    break;
            }

        }
        foodObject.transform.SetParent(gameObject.transform);
        return foodObject;
    }

    private GameObject InstantiateFood( InGameSprites inGameSprites)
    {
        GameObject foodObject;

        switch (inGameSprites)
        {
            case InGameSprites.MassBurner1:
                foodObject = Instantiate(massBurner1.gameObject);
                break;
            case InGameSprites.MassBurner2:
                foodObject = Instantiate(massBurner2.gameObject);
                break;
            case InGameSprites.MassGainer1:
                foodObject = Instantiate(massGainer1.gameObject);
                break;
            case InGameSprites.MassGainer2:
                foodObject = Instantiate(massGainer2.gameObject);
                break;
            default:
                foodObject=null;
                break;
        }
        return foodObject;
    }
}
