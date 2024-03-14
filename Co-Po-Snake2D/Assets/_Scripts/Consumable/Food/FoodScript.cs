using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    private ConsumableStates consumableState;
    [SerializeField] private int score;
    [SerializeField] private int bodyGrow;
    [SerializeField] private float foodLifeTime = 7f;

    public int Score { get => score; }
    public int BodyGrow { get => bodyGrow;}
    public ConsumableStates ConsumableState { get => consumableState; set => consumableState = value; }

    private void Start()
    {
        consumableState = ConsumableStates.Spawned;
        StartCoroutine(FoodLifeTime());
    }

    

    private IEnumerator FoodLifeTime()
    {
        float elapsedTime = 0f; // Track the elapsed time when paused

        while (elapsedTime < foodLifeTime)
        {
            if (GameHandler.State != GameStates.Pause) // Only count time if not paused
            { 
                elapsedTime += Time.deltaTime; 
            }

            yield return null; // Yield each frame
        }

        // If not in pause state when time is up, set state to Rotten
        if (GameHandler.State != GameStates.Pause)
            consumableState = ConsumableStates.Rotten;
    }
}
