using UnityEngine;

public class SnakeDeathScript : MonoBehaviour
{
    [SerializeField] private Snake snake;
    private PlayerEnum player;
    //powerups activation variables
    private bool canDie;
    public bool CanDie { get => canDie; set => canDie = value; }

    private void Awake()
    {
        CanDie = true;
        player = snake.Player;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag(PlayerEnum.Player1.ToString()) || collision.gameObject.CompareTag(PlayerEnum.Player2.ToString())) && collision.gameObject.tag != snake.gameObject.tag)
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
            bool isItASuicide = false; //default value

            if (otherSnake.Player != player && otherSnake.gameObject.GetComponent<SnakeDeathScript>().CanDie) // Player hit opponent
            {
                isItASuicide = false;
            }
            else if (otherSnake.Player == player && CanDie) // Player hit itself
            {
                isItASuicide = true;
            }

            GameHandler.GameResult = (isItASuicide, player);
            GameStateManager.GameState = GameStates.GameOver;
        }
    }

}
