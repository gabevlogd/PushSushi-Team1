using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
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
