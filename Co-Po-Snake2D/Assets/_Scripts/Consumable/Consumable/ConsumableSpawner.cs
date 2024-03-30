using UnityEngine;

public class ConsumableSpawner : MonoBehaviour
{
    private const int SPAWN_AREA_WIDTH = 200; //it's localScale will be used as spawned area.
    private const int SPAWN_AREA_HEIGHT = 200;
    private const float CHECKING_RADIUS = 1.0f;

    protected Vector2 GenerateRandomPosition()
    {
        Vector2 randomPosition;
        bool positionIsValid;

        do
        {
            randomPosition = new Vector2(Random.Range(0, SPAWN_AREA_WIDTH), Random.Range(0, SPAWN_AREA_HEIGHT));

            // Check if there are any colliders overlapping with the randomly generated position
            Collider2D[] colliders = Physics2D.OverlapCircleAll(randomPosition, CHECKING_RADIUS);
            positionIsValid = colliders.Length == 0;

        } while (!positionIsValid);

        return randomPosition;
    }
}
