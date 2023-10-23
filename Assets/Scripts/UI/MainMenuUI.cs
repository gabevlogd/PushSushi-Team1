using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : UIWindow
{
    public Button Option;
    public Button Start;
    public Button Skin;
    public Button Level;

    private void Awake()
    {
        Skin.onClick.AddListener(PerformSkinsButton);
        Level.onClick.AddListener(PerformLevelButton);
        Start.onClick.AddListener(PerformStartButton);
    }

    private void PerformLevelButton() => ChangeWindow(FindObjectOfType<LevelMenuUI>(true));

    private void PerformStartButton()
    {
        LevelLoader.LevelToLoad = PlayerData.LastSelectedLevel;
        SceneManager.LoadScene("GameScene");
    }
}
