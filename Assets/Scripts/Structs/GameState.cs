using System.Collections.Generic;

public struct GameState
{
    public List<ObjectState> ObjectStates;
    
    public GameState(List<ObjectState> objStates)
    {
        ObjectStates = objStates;
    }
}