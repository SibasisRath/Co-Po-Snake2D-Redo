using UnityEngine;

public class SnakeBodyPart
{
    private GameObject snakeBodyGameObject;
    private Transform transform;
    private Vector2Int gridPosition;

    public GameObject SnakeBodyGameObject { get => snakeBodyGameObject; set => snakeBodyGameObject = value; }

    public SnakeBodyPart(int bodyIndex, Snake player)
    {
        SnakeBodyGameObject = GameObject.Instantiate(player.SnakeBody);
        SnakeBodyGameObject.transform.parent = player.transform;
        SnakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
        transform = SnakeBodyGameObject.transform;
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
        GameObject.Destroy(this.SnakeBodyGameObject);
    }

}
