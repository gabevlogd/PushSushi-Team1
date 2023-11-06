using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    private UIWindow _previousWindow;

    protected virtual void OnEnable()
    {
        //Debug.Log(PreviousWindow);
        //Debug.Log(CurrentWindow);

    }

    protected void OpenTab(GameObject tabToOpen)
    {
        tabToOpen.SetActive(true);
        SoundManager.ButtonSound?.Invoke();
    }

    protected void CloseTab(GameObject tabToClose)
    {
        tabToClose.SetActive(false);
        SoundManager.ButtonSound?.Invoke();
    }

    protected void PerformBackButton() => ChangeWindow(_previousWindow);


    protected virtual void ChangeWindow(UIWindow windowToOpen)
    {
        if (windowToOpen._previousWindow == null)
            windowToOpen._previousWindow = this;

        windowToOpen.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
        SoundManager.ButtonSound?.Invoke();
    }

    protected virtual void ChangeTab(GameObject currentTab, GameObject tabToOpen)
    {
        currentTab?.SetActive(false);
        tabToOpen.SetActive(true);
        SoundManager.ButtonSound?.Invoke();
    }

}
