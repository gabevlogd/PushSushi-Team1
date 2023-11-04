using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUIComponent : MonoBehaviour
{
    public Image LevelImage;
    public Image ScoreImage;
    public Image SelectionFeedback;
    public TextMeshProUGUI LevelIndexText;

    public List<Sprite> ScoreSprite;
    public List<Sprite> LevelSprite;

    public int LevelIndex;

    private Button _button;

    private LevelData _currentLevel;


    public static event Action<int> OnLevelComponentClick;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PerformLevelSelection);
        //_currentLevel = LevelLoader.GetLevel(PlayerData.CurrentSelectedLevel.Theme, PlayerData.CurrentSelectedLevel.Difficulty, LevelIndex);
        //SetGraphic();
    }

    private void OnEnable()
    {
        SelectionFeedback.gameObject.SetActive(false);
        SetGraphic();
    }

    private void PerformLevelSelection()
    {
        PlayerData.CurrentSelectedLevel.LevelIndex = int.Parse(LevelIndexText.text);
        SelectionFeedback.gameObject.SetActive(true);
        OnLevelComponentClick?.Invoke(PlayerData.CurrentSelectedLevel.LevelIndex);
    }

    private void SetGraphic()
    {
        _currentLevel = LevelLoader.GetLevel(PlayerData.CurrentSelectedLevel.Theme, PlayerData.CurrentSelectedLevel.Difficulty, LevelIndex);

        if (_currentLevel == null || _currentLevel.LevelIndex == 1) return;

        bool levelCompleted = SaveManager.GetLevelDataBool(_currentLevel, Constants.LEVEL_COMPLETED);
        if (!levelCompleted)
        {
            LevelImage.sprite = LevelSprite[1];
            LevelIndexText.gameObject.SetActive(false);
        }
    }
}
