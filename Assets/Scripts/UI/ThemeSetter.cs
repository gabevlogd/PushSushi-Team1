using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ThemeSetter : MonoBehaviour
{
    public List<GameObject> Maps;
    public List<GameObject> Backgrounds;
    public List<HUDData> HUD;

    public static event Action<HUDData> OnHUDActivation;
    private LevelData _currentLevel;

    private void Awake()
    {
        _currentLevel = LevelLoader.LevelToLoad;
        SetUpTheme();
    }

    private void SetUpTheme()
    {
        SetMap();
        SetBackground();
        SetHUD();
    }

    private void SetBackground() => Backgrounds[(int)_currentLevel.Theme].SetActive(true);
    private void SetMap() => Instantiate(Maps[(int)_currentLevel.Theme]);

    private void SetHUD()
    {
        HUDData hud = HUD[(int)_currentLevel.Theme];
        hud.gameObject.SetActive(true);
        OnHUDActivation?.Invoke(hud);
    }
}