using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverTab : MonoBehaviour
{
    public Sprite[] ScoreSprites;
    public Image ScoreImage;
    public Image CrownImage;
    public TextMeshProUGUI ResultLabel;
    public Button Restart;
    public Button Skins;
    public Button Close;
    public Button Stages;

    public GameObject SkinsTab;

    private LevelData _currentLevel;

    private void Awake()
    {
        Skins.onClick.AddListener(delegate { OpenTab(SkinsTab); });
        Close.onClick.AddListener(delegate { CloseTab(SkinsTab); });
        Restart.onClick.AddListener(PerformRestart);
        Stages.onClick.AddListener(PerformStagesButton);

        _currentLevel = LevelLoader.LevelToLoad;

        SoundManager.GameOverSound?.Invoke();
    }

    private void OnEnable()
    {
        ResultLabel.text = GetResultLabel();
        ScoreImage.sprite = GetScoreSprite();
        CrownImage.gameObject.SetActive(CrownGained());
    }

    private void OpenTab(GameObject TabToOpen)
    {
        SoundManager.ButtonSound?.Invoke();
        TabToOpen.SetActive(true);
    }

    private void CloseTab(GameObject TabToClose)
    {
        SoundManager.ButtonSound?.Invoke();
        TabToClose.SetActive(false);
    }

    private void PerformRestart()
    {
        SoundManager.ButtonSound?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private string GetResultLabel()
    {
        string resultLabel;
        if (_currentLevel.Score == Score.Crown)
            resultLabel = "Perfect!";
        else
            resultLabel = _currentLevel.Score.ToString();
        return resultLabel;
    }

    private Sprite GetScoreSprite()
    {
        if (_currentLevel.Score == Score.Crown)
            return ScoreSprites[ScoreSprites.Length -1];
        else 
            return ScoreSprites[(int)_currentLevel.Score];
    }

    private bool CrownGained()
    {
        if (_currentLevel.Score == Score.Crown)
            return true;
        else return false;
    }

    private void PerformStagesButton()
    {
        MainMenuUI.GoToLevelWindow = true;
        SceneManager.LoadScene("MenuScene");
    }
}
