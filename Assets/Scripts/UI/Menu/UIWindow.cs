using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    public UIWindow PreviousWindow;

    protected virtual void OnEnable()
    {
        //Debug.Log(PreviousWindow);
        //Debug.Log(CurrentWindow);

    }

    protected void OpenTab(GameObject tabToOpen) => tabToOpen.SetActive(true);

    protected void CloseTab(GameObject tabToClose) => tabToClose.SetActive(false);

    protected void PerformBackButton() => ChangeWindow(PreviousWindow);


    protected virtual void ChangeWindow(UIWindow windowToOpen)
    {
        if (windowToOpen.PreviousWindow == null)
            windowToOpen.PreviousWindow = this;

        windowToOpen.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    protected virtual void ChangeTab(GameObject currentTab, GameObject tabToOpen)
    {
        currentTab?.SetActive(false);
        tabToOpen.SetActive(true);
    }

}
