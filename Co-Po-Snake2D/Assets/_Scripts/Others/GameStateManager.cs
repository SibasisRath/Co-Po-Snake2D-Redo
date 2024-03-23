using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private static GameStates gameState;
    [SerializeField] private KeyCode pauseKey = KeyCode.P;

    public static GameStates GameState { get => gameState; set => gameState = value; }


    // Start is called before the first frame update
    void Start()
    {
        GameState = GameStates.Running;
    }

    private void Update()
    {
        PauseCheck();
        UpdateTimeScale();
    }

    private static void UpdateTimeScale()
    {
        if (GameState == GameStates.Running)
        {
            Time.timeScale = 1;
        }

        else
        {
            Time.timeScale = 0;
        }
    }

    private void PauseCheck()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            GameState = GameStates.Pause;
        }
    }
}
