using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    [SerializeField] private InGameSprites powerUpSprite;
    [SerializeField] private float effectiveTime; // If snake take this power up. Then this long it's effect will be present with the player. 

    public InGameSprites PowerUpSprite { get => powerUpSprite; }
    public float EffectiveTime { get => effectiveTime; }
}
