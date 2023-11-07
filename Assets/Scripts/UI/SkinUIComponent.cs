using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinUIComponent : MonoBehaviour
{
    public SlidableComponent SkinPrefab;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PerformSkinSelection);
    }

    private void PerformSkinSelection()
    {
        MenuData.SelectedSkin = SkinPrefab;
        PerformSelectionFeedback();
    }

    private void PerformSelectionFeedback()
    {
        SoundManager.ButtonSound?.Invoke();
        foreach(Transform child in transform.parent)
        {
            if (child == transform) child.GetChild(0).gameObject.SetActive(true);
            else child.GetChild(0).gameObject.SetActive(false);
        }
    }
}
