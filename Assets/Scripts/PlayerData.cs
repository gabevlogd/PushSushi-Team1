using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public static Difficulty LastSelectedDifficulty;
    public static int LastSelectedLevelIndex;
    public static int LastCompletedLevelIndex;
    private int _coinCounter;

    public PlayerData()
    {
        Coin.OnUpdateCoinCounter += UpdateCoinCounter;
    }

    ~PlayerData()
    {
        Coin.OnUpdateCoinCounter -= UpdateCoinCounter;
    }

    private void UpdateCoinCounter(int coinToAdd)
    {
        _coinCounter += coinToAdd;
        Debug.Log(_coinCounter);
    }
}
