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
    private LevelData _currentLevel;

    private void Awake()
    {
        _levelDataHandler = new LevelDataHandler();
        _levelDataHandler.LoadLevel(1, out _currentLevel);
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

    private void CleanBoard()
    {
        foreach (SlidableComponent pawn in FindObjectsByType<SlidableComponent>(FindObjectsSortMode.None))
            Destroy(pawn.gameObject);
    }

    #region PLACEHOLDER FOR SECOND BUILD
    public void LoadNextLevel()
    {
        //Scene currentScene = SceneManager.GetActiveScene();
        //if (int.Parse(currentScene.name) + 1 > 15) return;
        //SceneManager.LoadScene($"{int.Parse(currentScene.name) + 1}");
        if (_currentLevel.LevelIndex > Resources.LoadAll<LevelData>("Beginner/").Length || _currentLevel.LevelIndex < 1) 
            return;

        CleanBoard();
        _levelDataHandler.LoadLevel(_currentLevel.LevelIndex + 1, out _currentLevel);
        
    }

    public void LoadPreviousLevel()
    {
        //Scene currentScene = SceneManager.GetActiveScene();
        //if (int.Parse(currentScene.name) - 1 < 1) return;
        //SceneManager.LoadScene($"{int.Parse(currentScene.name) - 1}");

        if (_currentLevel.LevelIndex > Resources.LoadAll<LevelData>("Beginner/").Length || _currentLevel.LevelIndex < 1) 
            return;

        CleanBoard();
        _levelDataHandler.LoadLevel(_currentLevel.LevelIndex - 1, out _currentLevel);
    }

    private void OnEnable() => SlidableComponent.OnLevelComplete += LoadNextLevel; 
    private void OnDisable() => SlidableComponent.OnLevelComplete -= LoadNextLevel; 
    


    #endregion
}