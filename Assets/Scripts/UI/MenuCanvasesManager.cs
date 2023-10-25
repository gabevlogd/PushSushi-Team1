using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvasesManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject LevelMenu;
    public GameObject LevelGridMenu;

    public static bool StartFromLevel;

    private void Awake()
    {
        if (StartFromLevel)
        {
            LevelMenu.SetActive(true);
            StartFromLevel = false;
        }
        else
            MainMenu.SetActive(true);


    }

}
