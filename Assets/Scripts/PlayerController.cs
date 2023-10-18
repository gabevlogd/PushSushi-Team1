using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public UndoManager UndoButton;
    
    public void Move()
    {
        GameState currentState = CaptureCurrentGameState();
        UndoButton.RecordGameState(currentState);
    }
    
    GameState CaptureCurrentGameState()
    {
        int currentMoveCount = UndoButton.GetMovesCounter();
        List<ObjectState> currentObjectStates = new List<ObjectState>();

        return new GameState(currentObjectStates);
    }
}