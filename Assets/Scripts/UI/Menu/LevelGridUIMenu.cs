using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelGridUIMenu : UIWindow
{
    public TextMeshProUGUI Difficulty;
    public TextMeshProUGUI LevelLocked;
    public Button Back;
    public Button Start;
    public Button Skins;
    public Button Close;

    public GameObject SkinsTab;

    private void Awake()
    {
        Back.onClick.AddListener(PerformBackButton);
        Start.onClick.AddListener(delegate { PerformStartButton(MenuData.CurrentSelectedLevel); });
        Skins.onClick.AddListener(delegate { OpenTab(SkinsTab); });
        Close.onClick.AddListener(delegate { CloseTab(SkinsTab); });
    }

    private void OnEnable() => Difficulty.text = MenuData.CurrentSelectedLevel.Difficulty.ToString();

    private void PerformStartButton(LevelData levelToLoad)
    {
        SoundManager.ButtonSound?.Invoke();
        LevelLoader.LevelToLoad = LevelLoader.GetLevel(levelToLoad.Theme, levelToLoad.Difficulty, levelToLoad.LevelIndex);
        if (LevelLoader.LevelToLoad == null)
        {
            StartCoroutine(LevelLockedMessage());
            return;
        }
        MenuData.LastSelectedLevel = LevelLoader.LevelToLoad;
        SceneManager.LoadScene("GameScene");
    }


    private IEnumerator LevelLockedMessage()
    {
        float deltaTime = 0.5f;
        float alpha = 1f;

        LevelLocked.color = new Color(LevelLocked.color.r, LevelLocked.color.g, LevelLocked.color.b, alpha);
        yield return new WaitForSeconds(deltaTime);

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime;
            LevelLocked.color = new Color(LevelLocked.color.r, LevelLocked.color.g, LevelLocked.color.b, alpha);
            yield return null;
        }
    }

}
