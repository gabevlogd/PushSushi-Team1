using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private UndoManager Undo;
    private LevelData _currentLevel;

    private void Awake()
    {

        ///temporary
        if (LevelLoader.LevelToLoad == null)
            LevelLoader.LevelToLoad = LevelLoader.GetLevel(Theme.Sushi, Difficulty.Beginner, 1);
        ///temporary
        LevelLoader.LoadLevel(LevelLoader.LevelToLoad.Theme, LevelLoader.LevelToLoad.Difficulty, LevelLoader.LevelToLoad.LevelIndex, out _currentLevel);
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

    /// <summary>
    /// Loads the next level after the current one
    /// </summary>
    public void LoadNextLevel()
    {
        if (_currentLevel == null) return;
        if (_currentLevel.LevelIndex >= LevelLoader.GetLevels(_currentLevel.Theme, _currentLevel.Difficulty).Length)
            return;

        LevelLoader.LevelToLoad = LevelLoader.GetLevel(_currentLevel.Theme, _currentLevel.Difficulty, _currentLevel.LevelIndex + 1);
        PlayerData.LastSelectedLevel = LevelLoader.LevelToLoad;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //reset all MonoBehaviour in the scene
    }

    /// <summary>
    /// Loads the previous level before the current one
    /// </summary>
    public void LoadPreviousLevel()
    {
        if (_currentLevel == null) return;
        if (_currentLevel.LevelIndex == 1)
            return;

        LevelLoader.LevelToLoad = LevelLoader.GetLevel(_currentLevel.Theme, _currentLevel.Difficulty, _currentLevel.LevelIndex - 1);
        PlayerData.LastSelectedLevel = LevelLoader.LevelToLoad;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //reset all MonoBehaviour in the scene
    }

    ///// <summary>
    ///// Remove all the gameobject on the grid
    ///// </summary>
    //private void CleanBoard()
    //{
    //    foreach (SlidableComponent pawn in FindObjectsByType<SlidableComponent>(FindObjectsSortMode.None))
    //        Destroy(pawn.gameObject);
    //    foreach (Coin coin in FindObjectsByType<Coin>(FindObjectsSortMode.None))
    //        Destroy(coin.gameObject);
    //}


    #region PLACEHOLDER FOR SECOND BUILD
    
    private void OnEnable() => SlidableComponent.OnLevelComplete += LoadNextLevel; 
    private void OnDisable() => SlidableComponent.OnLevelComplete -= LoadNextLevel;
    //public static int GetCurrentLevelIndex()
    //{
    //    if (_currentLevel == null) return 0;
    //    return _currentLevel.LevelIndex;
    //}

    #endregion
}