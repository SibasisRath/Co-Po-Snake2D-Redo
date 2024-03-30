using System.Collections;
using UnityEngine;

public class FoodScript : MonoBehaviour, IConsumable
{
    [SerializeField] protected int score;
    [SerializeField] protected int bodyUnitsToChange;
    [SerializeField] private float foodLifeTime = 7f;

    private Snake snake;

    private void Start()
    {
        StartCoroutine(FoodLifeTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        snake = collision.GetComponent<Snake>();
        if(snake != null)
        {
            Consumed(snake);
        }
    }

    private IEnumerator FoodLifeTime()
    {
        yield return new WaitForSeconds(foodLifeTime);
        Destroy(gameObject);
    }

    public virtual void Consumed(Snake snake){}
}
