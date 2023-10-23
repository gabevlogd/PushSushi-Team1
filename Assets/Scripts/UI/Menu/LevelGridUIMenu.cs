using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelGridUIMenu : UIWindow
{
    public TextMeshProUGUI Difficulty;
    public Button Back;
    public Button Start;
    public Button Skins;
    public Button Close;

    public GameObject SkinsTab;

    private void Awake()
    {
        Back.onClick.AddListener(PerformBackButton);
        Start.onClick.AddListener(delegate { PerformStartButton(PlayerData.CurrentSelectedLevel); });
        Skins.onClick.AddListener(delegate { OpenTab(SkinsTab); });
        Close.onClick.AddListener(delegate { CloseTab(SkinsTab); });
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Difficulty.text = PlayerData.CurrentSelectedLevel.Difficulty.ToString();
    }

    private void PerformStartButton(LevelData levelToLoad)
    {
        LevelLoader.LevelToLoad = LevelLoader.GetLevel(levelToLoad.Theme, levelToLoad.Difficulty, levelToLoad.LevelIndex);
        if (LevelLoader.LevelToLoad == null)
        {
            Debug.Log("Level Blocked");
            return;
        }
        PlayerData.LastSelectedLevel = LevelLoader.LevelToLoad;
        SceneManager.LoadScene("GameScene");
    }
}
