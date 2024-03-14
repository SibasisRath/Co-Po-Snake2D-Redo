using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelGrid
{
    private Vector2Int consumablesGridPosition;
    private int width;
    private int height;
    private int unitGrid;

    private GameObject food;
    private GameObject powerUps;
    private List<GameObject> foodGameObjects = new List<GameObject>();
    private List<GameObject> powerUpsGameObjects = new List<GameObject>();

   // private Snake snake; //this will be deleted
    private List<Snake> snakes = new List<Snake>();

    public LevelGrid(int width, int height, int unitGrid)
    {
        this.width = width;
        this.height = height;
        this.unitGrid = unitGrid;   
    }

    public void SnakeSetUp(Snake snake)
    {
        this.snakes.Add(snake);
       // this.snake = snake;
    }


    private int GenerateRandomNumber(int max)
    {
        // Here to adjust min and max and generate a random number which is divisible by unitGrid I am rounding up min and rounding down max respectively to the nearest multiple of unitGrid.
        //because min value is 0
        int adjustedMax = Mathf.FloorToInt((float)max / unitGrid);

        return (int)(Random.Range(0, adjustedMax + 1)) * unitGrid;
    }

    private Vector2Int SpawnlocationFinder()
    {
        do
        {
            consumablesGridPosition = new Vector2Int(GenerateRandomNumber(width), GenerateRandomNumber(height));
        } while (IsPositionOccupied(consumablesGridPosition));

        return consumablesGridPosition;
    }

    private bool IsPositionOccupied(Vector2Int position)
    {
        // Check if any snake occupies the position
        if (snakes.Any(snake => snake.GetSnakePositions().Contains(position)))
            return true;

        // Check if any food object occupies the position
        if (foodGameObjects.Any(obj => obj != null && obj.transform.position == new Vector3(position.x, position.y, 0)))
            return true;

        // Check if any power-up object occupies the position
        if (powerUpsGameObjects.Any(obj => obj != null && obj.transform.position == new Vector3(position.x, position.y, 0)))
            return true;

        return false;
    }

    public void SpawnFood()
    {
       
        Vector2Int foodGridPosition = SpawnlocationFinder();

        food = GameAssetManager.instance.GetFoodObject(snakes.Min(snake => snake.SnakeBodySize));
        food.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
        foodGameObjects.Add(food);
    }

    public void SpawnPowerUps()
    {
        Vector2Int powerUpGridPosition = SpawnlocationFinder();
        powerUps = GameAssetManager.instance.GetPowerUp();
        powerUps.transform.position = new Vector3(powerUpGridPosition.x, powerUpGridPosition.y);
        powerUpsGameObjects.Add(powerUps);
    }

    public void DestroyFood()
    {
        // Iterate through foodGameObjects
        for (int i = 0; i < foodGameObjects.Count; i++)
        {
            
            if (foodGameObjects[i] != null)
            {
                ConsumableStates consumableState = foodGameObjects[i].GetComponent<FoodScript>().ConsumableState;
                if (consumableState == ConsumableStates.Eaten || consumableState == ConsumableStates.Rotten)
                {
                    Object.Destroy(foodGameObjects[i]);
                }
            }
            
        }
        // Remove destroyed GameObjects from foodGameObjects list
        foodGameObjects.RemoveAll(obj => obj == null);
    }

    public bool CheckSnakeAteFood(Vector2Int snakePos, out FoodScript foodScript)
    {
        bool res = false;
        FoodScript foodScript1 = null;
        for (int i = 0; i < foodGameObjects.Count; i++)
        {
            if (foodGameObjects[i] != null && snakePos == new Vector2Int((int)foodGameObjects[i].transform.position.x, (int)foodGameObjects[i].transform.position.y))
            {
                foodScript1 = foodGameObjects[i].GetComponent<FoodScript>();
                foodScript1.ConsumableState = ConsumableStates.Eaten;
                res = true;
                break;
            }
            res = false;
        }
        foodScript = foodScript1;
        return res;
    }

    //in instruction there is no mention of destroying power ups. 
    public bool CheckSnakeAtePowerUp(Vector2Int snakePos, out GameObject powerUp)
    {
        bool res = false;
        GameObject resultPowerUp = null;

        for (int i = 0; i < powerUpsGameObjects.Count; i++)
        {
            if (powerUpsGameObjects[i] != null && snakePos == new Vector2Int((int)powerUpsGameObjects[i].transform.position.x, (int)powerUpsGameObjects[i].transform.position.y))
            {
                resultPowerUp = powerUpsGameObjects[i];
                res = true;
                break;
            }
            res = false;
        }
        powerUp = resultPowerUp;

        Object.Destroy(resultPowerUp);
        powerUpsGameObjects.RemoveAll(obj => obj == null);
        return res;
    }

    public Vector2Int ValidateGridPosition(Vector2Int gridPosition)
    {
        if (gridPosition.x<0)
        {
            gridPosition.x = width;
        }

        if (gridPosition.x > width)
        {
            gridPosition.x = 0;
        }

        if (gridPosition.y < 0)
        {
            gridPosition.y = height;
        }

        if (gridPosition.y > height)
        {
            gridPosition.y = 0;
        }

        return gridPosition;
    }

    public (bool,bool) SnakeDeathCheck(Vector2Int snakePos, PlayerEnum player)
    {
        bool isSnakeDead = false;
        bool isItASuicide = false;

        foreach (var snake in snakes)
        {
            // Check if snake's body contains the given position
            if (snake.GetSnakeBodyPositions().Contains(snakePos))
            {
                isSnakeDead = true;

                // Check if the snake belongs to the same player and if it's a suicide
                if (snake.Player == player)
                {
                    isItASuicide = true;
                }

                break; // No need to continue if we found a snake with the position
            }
        }

        return (isSnakeDead, isItASuicide);
    }

}
