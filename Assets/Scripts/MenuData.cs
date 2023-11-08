using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuData
{
    public static SlidableComponent SelectedSkin;
    public static LevelData LastSelectedLevel;
    public static LevelData CurrentSelectedLevel;
    private int _coinCounter;

    //public PlayerData()
    //{
    //    Coin.OnUpdateCoinCounter += UpdateCoinCounter;
    //}

    //~PlayerData()
    //{
    //    Coin.OnUpdateCoinCounter -= UpdateCoinCounter;
    //}

    //private void UpdateCoinCounter(int coinToAdd)
    //{
    //    _coinCounter += coinToAdd;
    //    Debug.Log(_coinCounter);
    //}
}
