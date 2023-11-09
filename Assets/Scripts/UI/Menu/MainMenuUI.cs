using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : UIWindow
{
    public Button Option;
    public Button Start;
    public Button Skins;
    public Button Level;
    public Button Close;

    public Image SelectedSkin;

    public GameObject SkinsTab;

    public static bool GoToLevelWindow;

    private void Awake()
    {
        Skins.onClick.AddListener(delegate { OpenTab(SkinsTab); });
        Close.onClick.AddListener(delegate { CloseTab(SkinsTab); });
        Option.onClick.AddListener(PerformOptionButton);
        Level.onClick.AddListener(PerformLevelButton);
        Start.onClick.AddListener(PerformStartButton);

        //una roba brutta che non sapevo come fare altrimenti, se poi scopro un metodo piu carino tolgo sta cacata :)
        if (GoToLevelWindow)
        {
            GoToLevelWindow = false;
            Level.onClick.Invoke();
        }
    }

    private void OnEnable()
    {
        SkinUIComponent.OnUpdateSelectedSkin += UpdateSelectedSkin;
        if (MenuData.SelectedSkinSprite != null)
            SelectedSkin.sprite = MenuData.SelectedSkinSprite;
    }

    private void OnDisable() => SkinUIComponent.OnUpdateSelectedSkin -= UpdateSelectedSkin;

    private void PerformOptionButton() => ChangeWindow(FindObjectOfType<OptionMenuUI>(true));

    private void PerformLevelButton() => ChangeWindow(FindObjectOfType<LevelMenuUI>(true));

    private void PerformStartButton()
    {
        SoundManager.ButtonSound?.Invoke();
        if (MenuData.LastSelectedLevel == null)
            MenuData.LastSelectedLevel = LevelLoader.GetLevel(Theme.Sushi, Difficulty.Beginner, 1);
        LevelLoader.LevelToLoad = MenuData.LastSelectedLevel;
        SceneManager.LoadScene("GameScene");
    }

    private void UpdateSelectedSkin(Sprite sprite) => SelectedSkin.sprite = sprite;
}
