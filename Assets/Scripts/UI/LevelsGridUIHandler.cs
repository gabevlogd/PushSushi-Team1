using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsGridUIHandler : MonoBehaviour
{
    private List<LevelUIComponent> _levels = new List<LevelUIComponent>();
    private int _lastSelectedLevel = -1;

    private void Awake()
    {
        LevelUIComponent[] levels = GetComponentsInChildren<LevelUIComponent>();
        for (int i = 0; i < levels.Length; i++)
        {
            _levels.Add(levels[i]);
            _levels[i].LevelIndexText.text = (i + 1).ToString();
        }

        LevelUIComponent.OnLevelComponentClick += PerformLevelSelection;
            
    }

    private void PerformLevelSelection(int selectedLevelIndex)
    {
        if (_lastSelectedLevel == -1)
        {
            _lastSelectedLevel = selectedLevelIndex;
            return;
        }

        _levels[_lastSelectedLevel - 1].SelectionFeedback.gameObject.SetActive(false);
        _lastSelectedLevel = selectedLevelIndex;

    }
}
