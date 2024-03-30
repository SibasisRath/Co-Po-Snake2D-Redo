using System.Collections;
using UnityEngine;

public class SpeedBoostPowerUpScript : PowerUpScript
{
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
        SettingOffPowerUpObject();

        yield return new WaitForSeconds(effectiveTime);

        snakeTimer.IsSpeedBoostPowerUpActivated = false;
        Destroy(gameObject);
    }
}