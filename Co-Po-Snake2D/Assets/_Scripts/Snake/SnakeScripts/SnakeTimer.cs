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
    public float TimeCounter { get => timeCounter; set => timeCounter = value; }
    public float MaxTime { get => maxTime; set => maxTime = value; }
    
    public bool IsSpeedBoostPowerUpActivated { get => isSpeedBoostPowerUpActivated; set => isSpeedBoostPowerUpActivated = value; }
    public float PowerUpSpeedSpeed { get => powerUpSpeedSpeed; set => powerUpSpeedSpeed = value; }


    // Start is called before the first frame update
    void Start()
    {
        IsSpeedBoostPowerUpActivated = false;
        TimeCounter = MaxTime;
        CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (IsSpeedBoostPowerUpActivated)
        {
            TimeCounter += Time.deltaTime * PowerUpSpeedSpeed;
        }
        else
        {
            TimeCounter += Time.deltaTime * baseSpeed;
        }

        if (TimeCounter > MaxTime)
        {
            CanMove = true;
            TimeCounter -= MaxTime;
        }
    }
}
