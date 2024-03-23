using System.Collections;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    //private ConsumableStates consumableState;
    [SerializeField] private int score;
    [SerializeField] private int bodyGrow;
    [SerializeField] private float foodLifeTime = 7f;

    public int Score { get => score; }
    public int BodyGrow { get => bodyGrow;}

    private void Start()
    {
        StartCoroutine(FoodLifeTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerScore playerScore = collision.gameObject.GetComponent<PlayerScore>();
        if(playerScore != null)
        {
            playerScore.UpdateScore(score);
            collision.gameObject.GetComponent<Snake>().AdditionalSnakeBodySize = bodyGrow;
            Destroy(gameObject);
        }
    }


    private IEnumerator FoodLifeTime()
    {
        yield return new WaitForSeconds(foodLifeTime);
        Destroy(gameObject);
    }
}
