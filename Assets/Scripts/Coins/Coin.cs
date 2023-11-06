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
    private MeshRenderer _meshRenderer;
    private float _newAlpha = 1f;
    private float _fadeOutSpeed = 0.5f;
    private bool _canFadeOut;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Update()
    {
        if (_canFadeOut)
            PerformMaterialFadeOut();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _animator.Play("A_Coin_01a");
        SoundManager.CoinSound?.Invoke();
        OnUpdateCoinCounter?.Invoke(_coinValue);
        GetComponent<Collider>().enabled = false;
    }

    public void StartFadeOut() => _canFadeOut = true;

    private void PerformMaterialFadeOut()
    {
        _newAlpha -= Time.deltaTime * _fadeOutSpeed;
        _meshRenderer.material.SetFloat("_Alpha", _newAlpha);
        if (_newAlpha <= 0)
            gameObject.SetActive(false);
    }
}
