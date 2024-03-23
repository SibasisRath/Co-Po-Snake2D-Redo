using System.Collections;
using UnityEngine;

public class SpeedBoostPowerUpScript : MonoBehaviour
{
    [SerializeField] private BoxCollider2D powerUpCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float effectiveTime; // If snake take this power up. Then this long it's effect will be present with the player. 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SnakeTimer snakeTimer = collision.gameObject.GetComponent<SnakeTimer>();
        if (snakeTimer != null)
        {
            StartCoroutine(PowerUpEffects(snakeTimer));
        }
    }

    private IEnumerator PowerUpEffects(SnakeTimer snakeTimer)
    {
        snakeTimer.IsSpeedBoostPowerUpActivated = true;
        powerUpCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(effectiveTime);

        snakeTimer.IsSpeedBoostPowerUpActivated = false;
        Destroy(gameObject);
    }
}
