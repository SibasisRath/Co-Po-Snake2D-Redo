using UnityEngine;

public class SnakeBodyPart
{
    private GameObject snakeBodyGameObject;
    private Transform transform;
    private Vector2Int gridPosition;

   public SnakeBodyPart(int bodyIndex, PlayerEnum player)
    {
        if (player == PlayerEnum.Player1)
        {
            snakeBodyGameObject = GameAssetManager.instance.GetAssetGameObject(InGameSprites.SnakeBodySegment1);
        }
        if (player == PlayerEnum.Player2)
        {
            snakeBodyGameObject = GameAssetManager.instance.GetAssetGameObject(InGameSprites.SnakeBodySegment2);
        }

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
