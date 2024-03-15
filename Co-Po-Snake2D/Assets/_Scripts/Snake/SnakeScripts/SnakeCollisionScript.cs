using UnityEngine;

public class SnakeCollisionScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            Debug.Log($"Player ate food: {collision.gameObject.name}");
        }
        if (collision.gameObject.CompareTag("ShieldPowerUp"))
        {
            Debug.Log($"Player got shield: {collision.gameObject.name}");
        }
        if (collision.gameObject.CompareTag("SpeedBoostPowerUp"))
        {
            Debug.Log($"Player got speed: {collision.gameObject.name}");
        }
        if (collision.gameObject.CompareTag("ScoreBoostPowerUp"))
        {
            Debug.Log($"Player got score: {collision.gameObject.name}");
        }
        if (collision.gameObject.CompareTag("Player1"))
        {
            Debug.Log($"Player hit himself: {collision.gameObject.name}");
        }
        if (collision.gameObject.CompareTag("Player2"))
        {
            Debug.Log($"Player hit opponent: {collision.gameObject.name}");
        }
    }
}
