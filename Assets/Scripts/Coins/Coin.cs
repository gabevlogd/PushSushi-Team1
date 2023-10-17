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
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        _animator.Play("A_Coin_01a");
        OnUpdateCoinCounter?.Invoke(_coinValue);
        //gameObject.SetActive(false);
    }

    public void DisableCoin() => gameObject.SetActive(false);
}
