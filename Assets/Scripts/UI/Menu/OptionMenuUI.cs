using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class OptionMenuUI : UIWindow
{
    [SerializeField]
    private Button _back;
    [SerializeField]
    private Button _resetGameData;
    [SerializeField]
    private Button _unlockLevels;
    [SerializeField]
    private Button _yes;
    [SerializeField]
    private Button _no;
    [SerializeField]
    private Toggle _music;
    [SerializeField]
    private Toggle _sound;
    [SerializeField]
    private TextMeshProUGUI _confirmMessage;
    [SerializeField]
    private GameObject _confirmTab;

    public static event UnityAction<bool> OnToggleMusic;
    public static event UnityAction<bool> OnToggleSound;

    private void Awake()
    {
        string resetMsg = "Are you sure you want to reset all game data?";
        string unlockMsg = "Are you sure you want to unlock all available levels?";

        _back.onClick.AddListener(PerformBackButton);
        _resetGameData.onClick.AddListener(delegate { OpenConfirmTab(DeleteGameData, resetMsg); });
        _unlockLevels.onClick.AddListener(delegate { OpenConfirmTab(UnlockAllLevels, unlockMsg); });
        _no.onClick.AddListener(delegate { CloseTab(_confirmTab); });
        _music.onValueChanged.AddListener(OnToggleMusic);
        _sound.onValueChanged.AddListener(OnToggleSound);

        _music.isOn = SoundManager._musicOn;
        _sound.isOn = SoundManager._soundOn;
    }

    

    private void OpenConfirmTab(UnityAction actionToConfirm, string confirmMessage)
    {
        _confirmMessage.text = confirmMessage;
        _yes.onClick.RemoveAllListeners();
        _yes.onClick.AddListener(actionToConfirm);
        OpenTab(_confirmTab);
    }

    private void DeleteGameData()
    {
        PlayerPrefs.DeleteAll();
        CloseTab(_confirmTab);
    }

    private void UnlockAllLevels()
    {

        for (int theme = 0; theme < 3; theme++)
        {
            for (int difficulty = 0; difficulty < 3; difficulty++)
            {
                for (int levelIndex = 1; levelIndex < 31; levelIndex++)
                {
                    LevelData level = LevelLoader.GetLevel((Theme)theme, (Difficulty)difficulty, levelIndex);
                    if (level == null) break;
                    SaveManager.SetLevelDataInt(level, Constants.LEVEL_COMPLETED, 1);
                }
            }
        }

        CloseTab(_confirmTab);
    }
}
