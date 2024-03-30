using System.Collections;
using UnityEngine;

public class ShieldPowerUpScript : PowerUpScript
{
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
        SettingOffPowerUpObject();

        yield return new WaitForSeconds(effectiveTime);

        snake.CanDie = true;
        Destroy(gameObject);
    }
}