using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private UndoManager Undo;

    private void Start()
    {
        if (Undo == null)
        {
            Undo = FindObjectOfType<UndoManager>();
        }
    }
    
    public void OnRestart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void OnUndo() => Undo.PerformUndo();

    #region PLACEHOLDER FOR SECOND BUILD
    public void LoadNextLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (int.Parse(currentScene.name) + 1 > 15) return;
        SceneManager.LoadScene($"{int.Parse(currentScene.name) + 1}");
    }

    public void LoadPreviousLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (int.Parse(currentScene.name) - 1 < 1) return;
        SceneManager.LoadScene($"{int.Parse(currentScene.name) - 1}");
    }

    private void OnEnable() => SlidableComponent.OnLevelComplete += LoadNextLevel; 
    private void OnDisable() => SlidableComponent.OnLevelComplete -= LoadNextLevel; 
    


    #endregion
}