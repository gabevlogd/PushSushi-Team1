using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Button RestartButton;

    public void OnRestart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    
}
