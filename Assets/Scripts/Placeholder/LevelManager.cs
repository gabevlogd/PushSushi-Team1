using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private UndoHandler Undo;
    
    public Button RestartButton;
    public Button UndoButton;

    private void Start()
    {
        if (Undo == null)
        {
            Undo = FindObjectOfType<UndoHandler>();
        }
    }
    
    public void OnRestart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void OnUndo() => Undo.PerformUndo();
}