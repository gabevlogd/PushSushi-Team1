using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{

    private HUDData _data;

    private void OnEnable()
    {
        SlidableComponent.OnLevelComplete += OpenGameOverTabRequest;
        ThemeSetter.OnHUDActivation += SetHUDData;
    }

    private void OnDisable()
    {
        SlidableComponent.OnLevelComplete -= OpenGameOverTabRequest;
        ThemeSetter.OnHUDActivation -= SetHUDData;
    }

    private void Start()
    {
        _data.Pause.onClick.AddListener(delegate { OpenTab(_data.PauseTab); });
        _data.Skins.onClick.AddListener(delegate { OpenTab(_data.SkinsTab); });
        _data.Resume.onClick.AddListener(delegate { CloseTab(_data.PauseTab); });
        _data.Close.onClick.AddListener(delegate { CloseTab(_data.SkinsTab); });
        _data.Restart.onClick.AddListener(delegate { PerformRestartButton(_data.PauseTab); });
        _data.Stages.onClick.AddListener(PerformStagesButton);
        _data.LevelCounter.text = LevelLoader.LevelToLoad.LevelIndex.ToString();
    }

    private void OpenTab(GameObject TabToOpen) => TabToOpen.SetActive(true);

    private void CloseTab(GameObject TabToClose) => TabToClose.SetActive(false);

    private void PerformRestartButton(GameObject TabToClose)
    {
        CloseTab(TabToClose);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PerformStagesButton()
    {
        MainMenuUI.GoToLevelWindow = true;
        SceneManager.LoadScene("MenuScene");
    }

    private void OpenGameOverTabRequest() => StartCoroutine(OpenGameOverTab());

    private IEnumerator OpenGameOverTab()
    {
        yield return new WaitForSeconds(1f);
        OpenTab(_data.GameOverTab);
    }

    private void SetHUDData(HUDData data) => _data = data;
}
