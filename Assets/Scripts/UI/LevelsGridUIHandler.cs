using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsGridUIHandler : MonoBehaviour
{
    private List<LevelUIComponent> _levels = new List<LevelUIComponent>();

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

    private void PerformLevelSelection()
    {
        //to implement...
    }
}
