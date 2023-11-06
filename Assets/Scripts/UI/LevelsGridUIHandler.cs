using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsGridUIHandler : MonoBehaviour
{
    private LevelUIComponent[] _levels;
    private int _lastSelectedLevel = -1;

    private void Awake()
    {
        _levels = GetComponentsInChildren<LevelUIComponent>();
        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i].LevelIndex = i + 1;
            _levels[i].LevelIndexText.text = (i + 1).ToString();
        }
    }

    

    private void OnEnable() => LevelUIComponent.OnLevelComponentClick += PerformLevelSelection;
    private void OnDisable() => LevelUIComponent.OnLevelComponentClick -= PerformLevelSelection;

    private void PerformLevelSelection(int selectedLevelIndex)
    {
        Debug.Log("ue");
        if (_lastSelectedLevel == -1)
        {
            _lastSelectedLevel = selectedLevelIndex;
            return;
        }
        if (_lastSelectedLevel == selectedLevelIndex)
            return;

        _levels[_lastSelectedLevel - 1].SelectionFeedback.gameObject.SetActive(false);
        _lastSelectedLevel = selectedLevelIndex;

    }
}
