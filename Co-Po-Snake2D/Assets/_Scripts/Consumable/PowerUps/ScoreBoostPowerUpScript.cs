using System.Collections;
using UnityEngine;

public class ScoreBoostPowerUpScript : PowerUpScript
{ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerScore playerScore = collision.gameObject.GetComponent<PlayerScore>();
        if (playerScore != null)
        {
            StartCoroutine(PowerUpEffects(playerScore));
        }
    }

    private IEnumerator PowerUpEffects(PlayerScore playerScore)
    {
        playerScore.ScoreBoostPowerUpIsActivated = true;
        SettingOffPowerUpObject();

        yield return new WaitForSeconds(effectiveTime);

        playerScore.ScoreBoostPowerUpIsActivated = false;
        Destroy(gameObject);
    }
}