using UnityEngine;

public class GameAssetManager : MonoBehaviour
{
    private const int snakeSmallBodyLimit = 5;
    private const int totalNumberOfMassGainerFoods = 2;
    private const int totalNumberOfFoods = 4;
    private const int totalNumberOfPowerUps = 3;
    private static GameAssetManager instance;

    public static GameAssetManager Instance { get => instance; set => instance = value; }

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

    [Header("Foods")]
    [SerializeField] private GameObject massGainer1;
    [SerializeField] private GameObject massGainer2;
    [SerializeField] private GameObject massBurner1;
    [SerializeField] private GameObject massBurner2;

    [Space]
    [Header("PowerUps")]
    [SerializeField] private GameObject speedBoostPowerUp;
    [SerializeField] private GameObject scoreBoostPowerUp;
    [SerializeField] private GameObject shildPowerUp;

    [Space]
    [Header("SnakeBodyParts")]
    [SerializeField] private GameObject snakeHeadPlayer1;
    [SerializeField] private GameObject snakeBodyPartPlayer1;
    [SerializeField] private GameObject snakeHeadPlayer2;
    [SerializeField] private GameObject snakeBodyPartPlayer2;


    //This function will be responsible for returning any kind of sprite game object. Because there are different type of sprites So there are different public functions are there in this script.
    private GameObject GetAssetGameObject(InGameSprites spriteName)
    {
        GameObject resultObject;
        switch (spriteName)
        {
            case InGameSprites.MassGainer1:
                resultObject = Instantiate(massGainer1);
                break;
            case InGameSprites.MassGainer2:
                resultObject = Instantiate(massGainer2);
                break;
            case InGameSprites.MassBurner1:
                resultObject = Instantiate(massBurner1);
                break;
            case InGameSprites.MassBurner2:
                resultObject = Instantiate(massBurner2);
                break;
            case InGameSprites.ScoreBoostPowerUp:
                resultObject = Instantiate(scoreBoostPowerUp);
                break;
            case InGameSprites.ShieldPowerUp:
                resultObject = Instantiate(shildPowerUp);
                break;
            case InGameSprites.SpeedBoostPowerUp:
                resultObject = Instantiate(speedBoostPowerUp);
                break;
            case InGameSprites.SnakeHeadPlayer1:
                resultObject = Instantiate(snakeHeadPlayer1);
                break;
            case InGameSprites.SnakeBodySegmentPlayer1:
                resultObject = Instantiate(snakeBodyPartPlayer1);
                break;
            case InGameSprites.SnakeHeadPlayer2:
                resultObject = Instantiate(snakeHeadPlayer2);
                break;
            case InGameSprites.SnakeBodySegmentPlayer2:
                resultObject = Instantiate(snakeBodyPartPlayer2);
                break;
            default:
                resultObject = null;
                Debug.Log("There are no such sprite in game.");
                break;
        }

        
        return resultObject;
    }

    public GameObject GetSnakeBodyPart(InGameSprites snakeBody) 
    {
        return GetAssetGameObject(snakeBody);
    }

    public GameObject GetSnakeHeadPart(PlayerEnum player)
    {
        GameObject snake;
        if (player == PlayerEnum.Player1)
        {
            snake = GetAssetGameObject(InGameSprites.SnakeHeadPlayer1);
        }
        else if (player == PlayerEnum.Player2)
        {
            snake = GetAssetGameObject(InGameSprites.SnakeHeadPlayer2);
        }
        else
        {
            snake = null;
        }
        return snake;
    }

    //According to the instruction there are 2 types of food should be implemented. (i) Mass gainer and (ii) mass burner.
    //In my Project I have taken 2 types of mass gainer and 2 types of mass burner with different masses can be gained and burnt.
    //yes, The values of the foods are flexible. These can be changed at any time.
    public GameObject GetFoodObject(int snakeSize)
    {
        GameObject foodObject;
        if (snakeSize < snakeSmallBodyLimit)
        {
            int a = Random.Range(0, totalNumberOfMassGainerFoods);
            foodObject = (a == 0) ? GetAssetGameObject(InGameSprites.MassGainer1) : GetAssetGameObject(InGameSprites.MassGainer2);
        }
        else
        {
            int a = Random.Range(0, totalNumberOfFoods);
            switch (a)
            {
                case 0:
                    foodObject = GetAssetGameObject(InGameSprites.MassBurner1);
                    break;
                case 1:
                    foodObject = GetAssetGameObject(InGameSprites.MassBurner2);
                    break;
                case 2:
                    foodObject = GetAssetGameObject(InGameSprites.MassGainer1);
                    break;
                case 3:
                    foodObject = GetAssetGameObject(InGameSprites.MassGainer2);
                    break;
                default:
                    foodObject = null;
                    Debug.Log("Food error.");
                    break;
            }

        }
        foodObject.transform.SetParent(gameObject.transform);
        return foodObject;
    }

    public GameObject GetPowerUp()
    {
        GameObject powerUp;
        int randomNumber = Random.Range(0, totalNumberOfPowerUps);
        switch (randomNumber)
        {
            case 0:
                powerUp = GetAssetGameObject(InGameSprites.ScoreBoostPowerUp);
                break;
            case 1:
                powerUp = GetAssetGameObject(InGameSprites.ShieldPowerUp);
                break;
            case 2:
                powerUp = GetAssetGameObject(InGameSprites.SpeedBoostPowerUp);
                break;
            default:
                powerUp = null;
                Debug.Log("no such power up available.");
                break;
        }
        return powerUp;
    }
}
