using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private UndoManager Undo;
    private LevelDataHandler _levelDataHandler;
    private static LevelData _currentLevel;

    private void Awake()
    {
        _levelDataHandler = new LevelDataHandler();

        //temporary
        if (_currentLevel == null)
            _currentLevel = LevelLoader.LevelToLoad;
        if (_currentLevel != null)
            _levelDataHandler.LoadLevel(_currentLevel.Theme, _currentLevel.Difficulty, _currentLevel.LevelIndex, out _currentLevel);
        //temporary
    }

    private void Start()
    {
        if (Undo == null)
        {
            Undo = FindObjectOfType<UndoManager>();
        }
    }

    public void OnRestart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void OnUndo() => Undo.PerformUndo();
    public void LoadNextLevel()
    {
        if (_currentLevel == null) return;
        if (_currentLevel.LevelIndex >= Resources.LoadAll<LevelData>($"{PlayerData.LastSelectedLevel.Theme}/{PlayerData.LastSelectedLevel.Difficulty}/").Length)
            return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        CleanBoard();
        _levelDataHandler.LoadLevel(_currentLevel.Theme, _currentLevel.Difficulty, _currentLevel.LevelIndex + 1, out _currentLevel);
    }
    public void LoadPreviousLevel()
    {
        if (_currentLevel == null) return;
        if (_currentLevel.LevelIndex == 1)
            return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        CleanBoard();
        _levelDataHandler.LoadLevel(_currentLevel.Theme, _currentLevel.Difficulty, _currentLevel.LevelIndex - 1, out _currentLevel);
    }


    private void CleanBoard()
    {
        foreach (SlidableComponent pawn in FindObjectsByType<SlidableComponent>(FindObjectsSortMode.None))
            Destroy(pawn.gameObject);
        foreach (Coin coin in FindObjectsByType<Coin>(FindObjectsSortMode.None))
            Destroy(coin.gameObject);
    }


    #region PLACEHOLDER FOR SECOND BUILD
    
    private void OnEnable() => SlidableComponent.OnLevelComplete += LoadNextLevel; 
    private void OnDisable() => SlidableComponent.OnLevelComplete -= LoadNextLevel;
    public static int GetCurrentLevelIndex()
    {
        if (_currentLevel == null) return 0;
        return _currentLevel.LevelIndex;
    }

    #endregion
}