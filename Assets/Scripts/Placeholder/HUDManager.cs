using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI MoveCounter;
    public TextMeshProUGUI LevelCounter;

    private void OnEnable()
    {
        //SlidableComponent.OnUpdateMoveCounter += UpdateMoveCounter;
        if(SceneManager.GetActiveScene().name.Length < 3)
            LevelCounter.text = SceneManager.GetActiveScene().name;
    }

    //private void OnDisable()
    //{
    //    //SlidableComponent.OnUpdateMoveCounter -= UpdateMoveCounter;
    //}

    //private void UpdateMoveCounter(int moveToAdd) => MoveCounter.text = (int.Parse(MoveCounter.text) + moveToAdd).ToString();
    
}
