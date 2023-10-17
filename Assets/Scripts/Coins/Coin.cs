using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Coin : MonoBehaviour, IPointerDownHandler
{
    public static event Action<int> OnUpdateCoinCounter;

    [SerializeField]
    private int _coinValue;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        OnUpdateCoinCounter?.Invoke(_coinValue);
        gameObject.SetActive(false);
    }
}
