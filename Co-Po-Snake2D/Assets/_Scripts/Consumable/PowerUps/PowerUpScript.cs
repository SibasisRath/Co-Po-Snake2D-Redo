using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    [SerializeField] protected BoxCollider2D powerUpCollider;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected float effectiveTime; // If snake take this power up. Then this long it's effect will be present with the player.

    protected void SettingOffPowerUpObject()
    {
        powerUpCollider.enabled = false;
        spriteRenderer.enabled = false;
    }
}
