using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinUIComponent : MonoBehaviour
{
    public SlidableComponent SkinPrefab;
    private Button _button;
    private Sprite _selectedSkin;

    public static event Action<Sprite> OnUpdateSelectedSkin;

    private void Awake()
    {
        _selectedSkin = GetComponent<Image>().sprite;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PerformSkinSelection);
    }

    private void PerformSkinSelection()
    {
        MenuData.SelectedSkin = SkinPrefab;
        MenuData.SelectedSkinSprite = _selectedSkin;
        OnUpdateSelectedSkin?.Invoke(_selectedSkin);
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
