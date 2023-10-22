using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public static LevelData LastSelectedLevel = Resources.Load<LevelData>("Sushi/Beginner/Level 1"); //Load<LevelData> temporary
    public static LevelData LastCompletedLevel;
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
