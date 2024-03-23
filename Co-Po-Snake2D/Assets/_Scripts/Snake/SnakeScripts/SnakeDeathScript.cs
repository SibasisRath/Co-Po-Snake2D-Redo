using UnityEngine;

public class SnakeDeathScript : MonoBehaviour
{
    [SerializeField] private Snake snake;
    private PlayerEnum player;
    //powerups activation variables
    private bool canDie;
    public bool CanDie
    {
        get => canDie;
        set 
        {
            canDie = value;
            Debug.Log($"can die: {canDie}");
        }
    }
    private void Awake()
    {
        canDie = true;
        player = snake.Player;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2")) && collision.gameObject.tag != snake.gameObject.tag)
        {
            return;
        }
        else
        {
            DeathCheck(collision);
        }
    }
    private void DeathCheck(Collider2D collision)
    {
        Snake otherSnake = collision.gameObject.GetComponentInParent<Snake>();

        if (otherSnake != null)
        {
            if (otherSnake.Player != player && otherSnake.gameObject.GetComponent<SnakeDeathScript>().CanDie) // Player hit opponent
            {
                GameHandler.GameResult = (false, player);
                GameStateManager.GameState = GameStates.GameOver;
            }
            else if (otherSnake.Player == player && CanDie) // Player hit itself
            {
                GameHandler.GameResult = (true, player);
                GameStateManager.GameState = GameStates.GameOver;
            }
        }
    }

}
