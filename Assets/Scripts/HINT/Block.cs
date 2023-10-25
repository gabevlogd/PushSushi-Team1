using UnityEngine;

public enum Orientation
{
    Horizontal,
    Vertical
}

public class Block : MonoBehaviour
{
    public Vector2Int Position { get; private set; }
    public Orientation Orientation { get; private set; }
    public int Length { get; private set; }

    public Block(Vector2Int position, Orientation orientation, int length)
    {
        Position = position;
        Orientation = orientation;
        Length = length;
    }
    
    public void MoveTo(Vector2Int newPosition)
    {
        Position = newPosition;
    }
}