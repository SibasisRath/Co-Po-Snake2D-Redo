using System.Collections;
using UnityEngine;

public class ScoreBoostPowerUpScript : MonoBehaviour
{
    [SerializeField] private BoxCollider2D powerUpCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float effectiveTime; // If snake take this power up. Then this long it's effect will be present with the player. 

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
        powerUpCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(effectiveTime);

        playerScore.ScoreBoostPowerUpIsActivated = false;
        Destroy(gameObject);
    }
}
