using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private CommandInvoker _commandInvoker;

    #region SINGLETON
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }
    #endregion
    
    public void OnBlockMoved(Block block, Vector2Int startPosition, Vector2Int endPosition)
    {
        MoveCommand moveCommand = new MoveCommand(block, startPosition, endPosition);
        _commandInvoker.ExecuteCommand(moveCommand);
    }
}

