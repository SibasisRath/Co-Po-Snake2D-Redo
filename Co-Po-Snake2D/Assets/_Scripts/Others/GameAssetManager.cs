using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssetManager : MonoBehaviour
{
    private const int snakeSmallBidyLimit = 5;
    private const int totalNumberOfMassGainerFoods = 2;
    private const int totalNumberOfFoods = 4;
    private const int totalNumberOfPowerUps = 3;
    public static GameAssetManager instance;

    private void Awake()
    {
        instance = this;
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
    [SerializeField] private GameObject snakeHead1;
    [SerializeField] private GameObject snakeBodyPart1;
    [SerializeField] private GameObject snakeHead2;
    [SerializeField] private GameObject snakeBodyPart2;

    public GameObject GetAssetGameObject(InGameSprites spriteName)
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
            case InGameSprites.SnakeBodySegment1:
                resultObject = Instantiate(snakeBodyPart1);
                break;
            case InGameSprites.SnakeBodySegment2:
                resultObject = Instantiate(snakeBodyPart2);
                break;
            default:
                resultObject = null;
                Debug.Log("There are no such sprite in game.");
                break;
        }

        
        return resultObject;
    }

    public GameObject GetFoodObject(int snakeSize)
    {
        GameObject foodObject;
        if (snakeSize < snakeSmallBidyLimit)
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
