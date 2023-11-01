using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    //public TextMeshProUGUI MoveCounter;
    //public TextMeshProUGUI LevelCounter;

    //public Button Pause;
    //public Button Skins;
    //public Button Stages;
    //public Button Resume;
    //public Button Restart;
    //public Button Close;

    //public GameObject PauseTab;
    //public GameObject SkinsTab;
    //public GameObject GameOverTab;
    public List<HUDData> HUD;

    private HUDData _data;

    private void Awake()
    {
        _data = HUD[(int)LevelLoader.LevelToLoad.Theme];
        _data.gameObject.SetActive(true);

        _data.Pause.onClick.AddListener(delegate { OpenTab(_data.PauseTab); });
        _data.Skins.onClick.AddListener(delegate { OpenTab(_data.SkinsTab); });
        _data.Resume.onClick.AddListener(delegate { CloseTab(_data.PauseTab); });
        _data.Close.onClick.AddListener(delegate { CloseTab(_data.SkinsTab); });
        _data.Restart.onClick.AddListener(delegate { PerformRestartButton(_data.PauseTab); });
        _data.Stages.onClick.AddListener(PerformStagesButton);
        
    }

    private void OnEnable()
    {
        _data.LevelCounter.text = LevelLoader.LevelToLoad.LevelIndex.ToString();
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
        OpenTab(_data.GameOverTab);
    }



}
