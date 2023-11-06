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
    public Sprite UnlockedLevelSprite;
    public Sprite LockedLevelSprite;
    [HideInInspector]
    public int LevelIndex;

    private Button _button;

    private LevelData _currentLevel;


    public static event Action<int> OnLevelComponentClick;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PerformLevelSelection);
    }

    private void OnEnable()
    {
        SelectionFeedback.gameObject.SetActive(false);
        SetIcon();
    }

    private void PerformLevelSelection()
    {
        PlayerData.CurrentSelectedLevel.LevelIndex = LevelIndex;
        SelectionFeedback.gameObject.SetActive(true);
        OnLevelComponentClick?.Invoke(LevelIndex);
    }

    private void SetIcon()
    {
        _currentLevel = LevelLoader.GetLevel(PlayerData.CurrentSelectedLevel.Theme, PlayerData.CurrentSelectedLevel.Difficulty, LevelIndex);
        if (_currentLevel == null)
        {
            SetUpLockedIcon();
            return;
        }

        bool levelCompleted = SaveManager.GetLevelDataBool(_currentLevel, Constants.LEVEL_COMPLETED);
        bool levelToComplete = SaveManager.GetLevelDataBool(_currentLevel, Constants.LEVLE_TO_COMPLETE);
        if (levelCompleted || levelToComplete || _currentLevel.LevelIndex == 1)
            SetUpUnlockedIcon(levelCompleted);
        else
            SetUpLockedIcon();
        
    }

    private Sprite GetScoreSprite(bool levelCompleted)
    {
        int bestMoves = SaveManager.GetLevelDataInt(_currentLevel, Constants.BEST_MOVES);
        if (levelCompleted && bestMoves == 0)
            ScoreImage.gameObject.SetActive(false);
        else if (levelCompleted)
            ScoreImage.gameObject.SetActive(true);
        else
            ScoreImage.gameObject.SetActive(false);

        int bestScore = SaveManager.GetLevelDataInt(_currentLevel, Constants.BEST_SCORE);
        if (bestScore == (int)Score.Crown)
            return ScoreSprite[ScoreSprite.Count - 1];
        return ScoreSprite[bestScore];
    }

    private void SetUpLockedIcon()
    {
        LevelImage.sprite = LockedLevelSprite;
        LevelIndexText.gameObject.SetActive(false);
        ScoreImage.gameObject.SetActive(false);
    }

    private void SetUpUnlockedIcon(bool levelCompleted)
    {
        LevelImage.sprite = UnlockedLevelSprite;
        LevelIndexText.gameObject.SetActive(true);
        ScoreImage.sprite = GetScoreSprite(levelCompleted);
    }
}
