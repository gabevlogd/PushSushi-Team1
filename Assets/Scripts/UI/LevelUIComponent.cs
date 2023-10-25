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
    public TextMeshProUGUI LevelIndexText;

    public List<Sprite> ScoreSpite;
    public List<Sprite> LevelSprite;

    private Button _button;

    public static event Action OnLevelComponentClick;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PerformLevelSelection);
    }

    private void PerformLevelSelection()
    {
        PlayerData.CurrentSelectedLevel.LevelIndex = int.Parse(LevelIndexText.text);
        OnLevelComponentClick?.Invoke();
    }
}
