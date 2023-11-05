using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuUI : UIWindow
{
    public Button Skins;
    public Button Back;
    public Button Beginner;
    public Button Intermediate;
    public Button Advanced;
    public Button Level;
    public Button Themes;
    public Button Sushi;
    public Button Penguin;
    public Button Sweet;
    public Button Close;

    public GameObject ThemesTab;
    public GameObject LevelTab;
    public GameObject SkinsTab;

    private void Awake()
    {
        PlayerData.CurrentSelectedLevel = ScriptableObject.CreateInstance<LevelData>();
        Back.onClick.AddListener(PerformBackButton);
        Skins.onClick.AddListener(delegate { OpenTab(SkinsTab); });
        Close.onClick.AddListener(delegate { CloseTab(SkinsTab); });
        //Close.onClick.AddListener(delegate { LevelLoader.SetSkin(LevelLoader.LevelToLoad); });
        Beginner.onClick.AddListener(delegate { PerformDifficultyButton(Difficulty.Beginner); });
        Intermediate.onClick.AddListener(delegate { PerformDifficultyButton(Difficulty.Intermediate); });
        Advanced.onClick.AddListener(delegate { PerformDifficultyButton(Difficulty.Advanced); });
        Level.onClick.AddListener(delegate { PerformChengeTabButton(ThemesTab, LevelTab); });
        Themes.onClick.AddListener(delegate { PerformChengeTabButton(LevelTab, ThemesTab); });
        Sushi.onClick.AddListener(delegate { PerformThemeButton(Theme.Sushi); });
        Penguin.onClick.AddListener(delegate { PerformThemeButton(Theme.Penguin); });
        Sweet.onClick.AddListener(delegate { PerformThemeButton(Theme.Sweet); });
    }

    private void PerformDifficultyButton(Difficulty difficulty)
    {
        PlayerData.CurrentSelectedLevel.Difficulty = difficulty;
        ChangeWindow(FindObjectOfType<LevelGridUIMenu>(true));
    }

    private void PerformThemeButton(Theme theme)
    {
        PlayerData.CurrentSelectedLevel.Theme = theme;
        PerformChengeTabButton(ThemesTab, LevelTab);
        Debug.Log($"{theme} theme selected");
    }

    private void PerformChengeTabButton(GameObject currentTab, GameObject TabToOpen) => ChangeTab(currentTab, TabToOpen);

}
