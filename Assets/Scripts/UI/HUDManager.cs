using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI MoveCounter;
    public TextMeshProUGUI LevelCounter;

    public Button Pause;
    public Button Skins;
    public Button Stages;
    public Button Resume;
    public Button Restart;
    public Button Close;

    public GameObject PauseTab;
    public GameObject SkinsTab;
    public GameObject GameOverTab;

    private void Awake()
    {
        Pause.onClick.AddListener(delegate { OpenTab(PauseTab); });
        Skins.onClick.AddListener(delegate { OpenTab(SkinsTab); });
        Resume.onClick.AddListener(delegate { CloseTab(PauseTab); });
        Close.onClick.AddListener(delegate { CloseTab(SkinsTab); });
        Restart.onClick.AddListener(delegate { PerformRestartButton(PauseTab); });
        Stages.onClick.AddListener(PerformStagesButton);
        
    }

    private void OnEnable()
    {
        LevelCounter.text = LevelLoader.LevelToLoad.LevelIndex.ToString();
        SlidableComponent.OnLevelComplete += OpenGameOverTabRequest;
    }

    private void OnDisable() => SlidableComponent.OnLevelComplete -= OpenGameOverTabRequest;

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
        OpenTab(GameOverTab);
    }



}
