using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ThemeSetter : MonoBehaviour
{
    public List<GameObject> Maps;
    //public List<GameObject> HUD;

    private LevelData _currentLevel;

    private void Awake()
    {
        _currentLevel = LevelLoader.LevelToLoad;
        SetMap();
        //SetHUD();
    }

    private void SetMap() => Instantiate(Maps[(int)_currentLevel.Theme]);
}