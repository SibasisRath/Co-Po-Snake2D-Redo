using System.Collections;
using UnityEngine;

public class PowerUpSpwaner : ConsumableSpawner
{
    private const int TOTAL_NUMBER_OF_POWER_UP = 3;

    [SerializeField] private float powerUpSpawnInterval = 10f;

    [Header("PowerUps")]
    [SerializeField] private SpeedBoostPowerUpScript speedBoostPowerUp;
    [SerializeField] private ScoreBoostPowerUpScript scoreBoostPowerUp;
    [SerializeField] private ShieldPowerUpScript shieldPowerUp;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPowerUpsCoroutine());
    }

    private IEnumerator SpawnPowerUpsCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerUpSpawnInterval);
            if (GameStateManager.GameState == GameStates.Running)
                SpawnPowerUps();
        }
    }

    private void SpawnPowerUps()
    {
        GameObject powerUp = GetPowerUp();
        powerUp.transform.SetParent(transform);
        powerUp.transform.position = GenerateRandomPosition();
    }

    public GameObject GetPowerUp()
    {
        GameObject powerUp;
        int randomNumber = Random.Range(0, TOTAL_NUMBER_OF_POWER_UP);
        switch (randomNumber)
        {
            case 0:
                powerUp = InstantiatePowerUp(InGameSprites.ScoreBoostPowerUp);
                break;
            case 1:
                powerUp = InstantiatePowerUp(InGameSprites.ShieldPowerUp);
                break;
            case 2:
                powerUp = InstantiatePowerUp(InGameSprites.SpeedBoostPowerUp);
                break;
            default:
                powerUp = null;
                break;
        }
        return powerUp;
    }

    private GameObject InstantiatePowerUp(InGameSprites inGameSprites)
    {
        GameObject foodObject;

        switch (inGameSprites)
        {
            case InGameSprites.SpeedBoostPowerUp:
                foodObject = Instantiate(speedBoostPowerUp.gameObject);
                break;
            case InGameSprites.ShieldPowerUp:
                foodObject = Instantiate(shieldPowerUp.gameObject);
                break;
            case InGameSprites.ScoreBoostPowerUp:
                foodObject = Instantiate(scoreBoostPowerUp.gameObject);
                break;
            default:
                foodObject = null;
                break;
        }
        return foodObject;
    }

}
