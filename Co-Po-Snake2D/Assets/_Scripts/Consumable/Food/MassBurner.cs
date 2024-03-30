public class MassBurner : FoodScript
{
    public override void Consumed(Snake snake)
    {
        PlayerScore playerScore = snake.gameObject.GetComponent<PlayerScore>();
        playerScore.UpdateScore(score);
        snake.DecreaseBody(bodyUnitsToChange);
        Destroy(gameObject);
    }
}
