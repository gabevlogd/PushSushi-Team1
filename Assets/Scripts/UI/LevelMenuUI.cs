using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuUI : UIWindow
{
    public Button Skin;
    public Button Back;

    private void Awake()
    {
        Back.onClick.AddListener(PerformBackButton);
        Skin.onClick.AddListener(PerformSkinsButton);
    }
}
