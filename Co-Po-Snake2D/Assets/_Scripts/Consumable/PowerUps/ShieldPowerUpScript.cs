using System.Collections;
using UnityEngine;

public class ShieldPowerUpScript : MonoBehaviour
{
    [SerializeField] private BoxCollider2D powerUpCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float effectiveTime; // If snake take this power up. Then this long it's effect will be present with the player. 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SnakeDeathScript snake = collision.gameObject.GetComponent<SnakeDeathScript>();
        if (snake != null)
        {
            StartCoroutine(PowerUpEffects(snake));
        }
    }

    private IEnumerator PowerUpEffects(SnakeDeathScript snake)
    {
        snake.CanDie = false;
        powerUpCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(effectiveTime);

        snake.CanDie = true;
        Destroy(gameObject);
    }
}
