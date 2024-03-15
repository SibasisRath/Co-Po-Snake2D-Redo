using UnityEngine;

public class SnakeBodyPart
{
    private GameObject snakeBodyGameObject;
    private Transform transform;
    private Vector2Int gridPosition;

   public SnakeBodyPart(int bodyIndex, PlayerEnum player)
    {
        snakeBodyGameObject = GameAssetManager.Instance.GetSnakeBodyPart(player);

        snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
        transform = snakeBodyGameObject.transform;
    }

    public void SetGridPosition(Vector2Int gridPosition)
    {
        this.gridPosition = gridPosition;
        transform.position = new Vector3(this.gridPosition.x, this.gridPosition.y);
    }

    public Vector2Int GetGridPosition()
    {
        return this.gridPosition;
    }

    public void DestroyGameObject()
    {
        GameObject.Destroy(this.snakeBodyGameObject);
    }

}
