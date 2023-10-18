using System.Collections.Generic;

public class UndoManager
{
    private List<GameState> _gameStates;
    
    public void Start()
    {
        _gameStates = new List<GameState>();
    }
    
    public void RecordGameState(GameState state)
    {
        _gameStates.Add(state);
    }

    public int MovesCounter => _gameStates.Count;
    
    public void UndoLastMove()
    {
        if (_gameStates.Count > 1)
        {
            GameState previousState = _gameStates[_gameStates.Count - 2];
            _gameStates.RemoveAt(_gameStates.Count - 1);
        }
    }
}