public class MassGainer : FoodScript
{
    public override void Consumed(Snake snake)
    {
        PlayerScore playerScore = snake.gameObject.GetComponent<PlayerScore>();
        playerScore.UpdateScore(score);
        snake.IncreaseBody(bodyUnitsToChange);
        Destroy(gameObject);
    }
}
