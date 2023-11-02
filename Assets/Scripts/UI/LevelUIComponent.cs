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

    private Button _button;

    public static event Action<int> OnLevelComponentClick;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PerformLevelSelection);
    }

    private void PerformLevelSelection()
    {
        PlayerData.CurrentSelectedLevel.LevelIndex = int.Parse(LevelIndexText.text);
        SelectionFeedback.gameObject.SetActive(true);
        OnLevelComponentClick?.Invoke(PlayerData.CurrentSelectedLevel.LevelIndex);
    }
}
