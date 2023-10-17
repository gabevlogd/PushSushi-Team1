using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI MoveCounter;

    private void OnEnable()
    {
        SlidableComponentDeprecated.OnUpdateMoveCounter += UpdateMoveCounter;
    }

    private void OnDisable()
    {
        SlidableComponentDeprecated.OnUpdateMoveCounter -= UpdateMoveCounter;
    }

    private void UpdateMoveCounter(int moveToAdd) => MoveCounter.text = (int.Parse(MoveCounter.text) + moveToAdd).ToString();
    
}
