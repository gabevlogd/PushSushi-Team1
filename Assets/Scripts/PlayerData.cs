using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public static LevelData LastSelectedLevel /*= LevelLoader.GetLevel(Theme.Sushi, Difficulty.Beginner, 1)*/; //temporary
    public static LevelData CurrentSelectedLevel = new LevelData();
    public static LevelData[,] LastCompletedLevel = new LevelData[2, 3];
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
