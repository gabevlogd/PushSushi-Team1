using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelGridUIMenu : UIWindow
{
    public TextMeshProUGUI Difficulty;
    public Button Back;
    public Button Start;
    public Button Skins;
    public Button Close;

    public GameObject SkinsTab;

    private void Awake()
    {
        Back.onClick.AddListener(PerformBackButton);
        Start.onClick.AddListener(delegate { PerformStartButton(PlayerData.CurrentSelectedLevel); });
        Skins.onClick.AddListener(delegate { OpenTab(SkinsTab); });
        Close.onClick.AddListener(delegate { CloseTab(SkinsTab); });
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Difficulty.text = PlayerData.CurrentSelectedLevel.Difficulty.ToString();
    }

    private void PerformStartButton(LevelData levelToLoad)
    {
        SoundManager.ButtonSound?.Invoke();
        LevelLoader.LevelToLoad = LevelLoader.GetLevel(levelToLoad.Theme, levelToLoad.Difficulty, levelToLoad.LevelIndex);
        if (LevelLoader.LevelToLoad == null)
        {
            StartCoroutine(LevelLockedMessage());
            return;
        }
        PlayerData.LastSelectedLevel = LevelLoader.LevelToLoad;
        SceneManager.LoadScene("GameScene");
    }

    #region PLACEHOLDER
    public TextMeshProUGUI LevelLocked;
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
    #endregion
}
