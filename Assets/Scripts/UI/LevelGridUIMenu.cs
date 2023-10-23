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

    private void Awake()
    {
        Back.onClick.AddListener(PerformBackButton);
        Start.onClick.AddListener(delegate { PerformStartButton(PlayerData.CurrentSelectedLevel); });
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Difficulty.text = PlayerData.CurrentSelectedLevel.Difficulty.ToString();
    }

    private void PerformStartButton(LevelData levelToLoad)
    {
        LevelLoader.LevelToLoad = LevelLoader.GetLevel(levelToLoad.Theme, levelToLoad.Difficulty, levelToLoad.LevelIndex);
        PlayerData.LastSelectedLevel = LevelLoader.LevelToLoad;
        Debug.Log(LevelLoader.LevelToLoad);
        SceneManager.LoadScene("GameScene");
    }
}
