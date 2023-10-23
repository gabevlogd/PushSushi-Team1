using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    protected static UIWindow _previousWindow;
    protected static UIWindow _currentWindow;



    protected void PerformSkinsButton()
    {
        Debug.Log("Open skin selction tab");
    }

    protected void PerformBackButton() => ChangeWindow(_previousWindow);

    protected virtual void ChangeWindow(UIWindow windowToOpen)
    {
        _previousWindow = this;
        _currentWindow = windowToOpen;
        windowToOpen.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    protected virtual void ChangeTab(GameObject currentTab, GameObject tabToOpen)
    {
        currentTab.SetActive(false);
        tabToOpen.SetActive(true);
    }
}
