using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private bool _undoUsed;
    private int _movesCounter;
    private int _optimalMoves;
    private LevelData _currentLevel;

    private void Awake()
    {
        _currentLevel = LevelLoader.LevelToLoad;
        _optimalMoves = _currentLevel.OptimalMoves;
        Debug.Log(_currentLevel.Score);
    }

    private void OnEnable()
    {
        LevelManager.OnPerformUndo += DecraseMoves;
        UndoManager.OnMoveStored += IncraseMoves;
    }

    private void OnDisable()
    {
        LevelManager.OnPerformUndo -= DecraseMoves;
        UndoManager.OnMoveStored -= IncraseMoves;
    }

    private void IncraseMoves()
    {
        _movesCounter++;
        if (LevelManager.GameState == GameState.GameOver)
            CalculateScore();
        //Debug.Log(_movesCounter);
    }

    private void DecraseMoves()
    {
        if (!_undoUsed)
            _undoUsed = true;
        if (_movesCounter > 0)
            _movesCounter--;
    }

    private void CalculateScore()
    {
        //Debug.Log(_movesCounter);
        if (_movesCounter <= _optimalMoves)
        {
            if (_undoUsed) _currentLevel.Score = Score.Perfect;
            else _currentLevel.Score = Score.Crown;
        }
        else if (_movesCounter <= _optimalMoves * 1.5f)
            _currentLevel.Score = Score.Great;
        else
            _currentLevel.Score = Score.Good;

        //Debug.Log(LevelLoader.LevelToLoad.Score);
    }


}


