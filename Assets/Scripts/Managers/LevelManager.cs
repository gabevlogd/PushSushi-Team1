using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //private UndoManager Undo;
    private LevelData _currentLevel;

    public static GameState GameState;
    public static event Action OnPerformUndo;

    private void Awake()
    {
        GameState = GameState.Play;

        ///temporary
        if (LevelLoader.LevelToLoad == null)
            LevelLoader.LevelToLoad = LevelLoader.GetLevel(Theme.Sushi, Difficulty.Beginner, 1);
        

        ///temporary
        LevelLoader.LoadLevel(LevelLoader.LevelToLoad.Theme, LevelLoader.LevelToLoad.Difficulty, LevelLoader.LevelToLoad.LevelIndex, out _currentLevel);

        GameState = GetGameState();
    }

    private void Start()
    {
        //if (Undo == null)
        //{
        //    Undo = FindObjectOfType<UndoManager>();
        //}
        SaveManager.SetLevelDataInt(_currentLevel, Constants.LEVLE_TO_COMPLETE, 1);
    }

    public void OnRestart()
    {
        if (GameState != GameState.Play) return;
        SoundManager.ButtonSound?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnUndo()
    {
        if (GameState != GameState.Play) return;
        //Undo.PerformUndo();
        OnPerformUndo?.Invoke();
    }

    /// <summary>
    /// Loads the next level after the current one
    /// </summary>
    public void LoadNextLevel()
    {
        if (_currentLevel == null) return;
        if (!SaveManager.GetLevelDataBool(_currentLevel, Constants.LEVEL_COMPLETED)) return;
        if (_currentLevel.LevelIndex >= LevelLoader.GetLevels(_currentLevel.Theme, _currentLevel.Difficulty).Length)
            return;
        SoundManager.ButtonSound?.Invoke();
        LevelLoader.LevelToLoad = LevelLoader.GetLevel(_currentLevel.Theme, _currentLevel.Difficulty, _currentLevel.LevelIndex + 1);
        MenuData.LastSelectedLevel = LevelLoader.LevelToLoad;
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
        SoundManager.ButtonSound?.Invoke();
        LevelLoader.LevelToLoad = LevelLoader.GetLevel(_currentLevel.Theme, _currentLevel.Difficulty, _currentLevel.LevelIndex - 1);
        MenuData.LastSelectedLevel = LevelLoader.LevelToLoad;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //reset all MonoBehaviour in the scene
    }

    private GameState GetGameState()
    {
        if (_currentLevel.Difficulty == Difficulty.Beginner && _currentLevel.LevelIndex == 1)
            return GameState.Tutorial;
        else return GameState.Play;
    }



}

public enum GameState
{
    Play,
    GameOver,
    Tutorial

}