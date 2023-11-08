#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasesHandler : MonoBehaviour
{
    public GameObject MainMenuCanvas;
    public GameObject LevelMenuCanvas;
    public GameObject LevelsGridMenuCanvas;

    private void Awake()
    {
        MainMenuCanvas.SetActive(true);
        LevelMenuCanvas.SetActive(false);
        LevelsGridMenuCanvas.SetActive(false);    }
}
#endif
