using System.Collections;
using UnityEngine;

public class SnakeTimer : MonoBehaviour
{
    [SerializeField] private float timeCounter;
    [SerializeField] private float maxTime;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float powerUpSpeedSpeed;
    [SerializeField] private bool canMove;
    private bool isSpeedBoostPowerUpActivated;

    public bool CanMove { get => canMove; set => canMove = value; }
    public float MaxTime { get => maxTime; set => maxTime = value; }
    
    public bool IsSpeedBoostPowerUpActivated { get => isSpeedBoostPowerUpActivated; set => isSpeedBoostPowerUpActivated = value; }
    public float PowerUpSpeedSpeed { get => powerUpSpeedSpeed; set => powerUpSpeedSpeed = value; }


    // Start is called before the first frame update
    void Start()
    {
        IsSpeedBoostPowerUpActivated = false;
        timeCounter = MaxTime;
        CanMove = true;
        StartCoroutine(UpdateCoroutine());
    }

    private IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            if (IsSpeedBoostPowerUpActivated)
            {
                timeCounter += Time.deltaTime * PowerUpSpeedSpeed;
            }
            else
            {
                timeCounter += Time.deltaTime * baseSpeed;
            }

            if (timeCounter > MaxTime)
            {
                CanMove = true;
                timeCounter -= MaxTime;
            }
            yield return null; // Yield to the next frame
        }
    }
}
