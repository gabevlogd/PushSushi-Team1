using UnityEngine;

public class MoveCommand : ICommand
{
    private Block _block;
    private Vector2Int _startPosition;
    private Vector2Int _endPosition;
    
    public MoveCommand(Block block, Vector2Int startPosition, Vector2Int endPosition)
    {
        this._block = block;
        this._startPosition = startPosition;
        this._endPosition = endPosition;
    }
    
    public void Execute()
    {
        _block.MoveTo(_endPosition);
    }

    public void Undo()
    {
        _block.MoveTo(_startPosition);
    }
}