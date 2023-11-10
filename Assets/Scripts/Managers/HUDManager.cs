using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class HUDManager : MonoBehaviour
{

    private HUDData _data;
    private LevelData _currentLevel;

    public static event UnityAction<bool> OnToggleMusic;
    public static event UnityAction<bool> OnToggleSound;

    private void Awake() => _currentLevel = LevelLoader.LevelToLoad;

    private void OnEnable()
    {
        SlidableComponent.OnLevelComplete += OpenGameOverTabRequest;
        ThemeSetter.OnHUDActivation += SetHUDData;
        UndoManager.OnMoveStored += IncraseMoves;
        UndoManager.OnMoveCanceled += DecraseMoves;
        LevelManager.OnPerformHint += ShowHintMSG;
    }

    private void OnDisable()
    {
        SlidableComponent.OnLevelComplete -= OpenGameOverTabRequest;
        ThemeSetter.OnHUDActivation -= SetHUDData;
        UndoManager.OnMoveStored -= IncraseMoves;
        UndoManager.OnMoveCanceled -= DecraseMoves;
        LevelManager.OnPerformHint -= ShowHintMSG;
    }

    private void Start()
    {
        _data.Music.isOn = SoundManager._musicOn;
        _data.Sound.isOn = SoundManager._soundOn;
        _data.Pause.onClick.AddListener(delegate { OpenTab(_data.PauseTab); });
        _data.Skins.onClick.AddListener(delegate { OpenTab(_data.SkinsTab); });
        _data.Resume.onClick.AddListener(delegate { CloseTab(_data.PauseTab); });
        _data.Close.onClick.AddListener(delegate { CloseTab(_data.SkinsTab); });
        _data.Restart.onClick.AddListener(PerformRestartButton);
        _data.Stages.onClick.AddListener(PerformStagesButton);
        _data.Music.onValueChanged.AddListener(OnToggleMusic);
        _data.Sound.onValueChanged.AddListener(OnToggleSound);
        InitHUD();
    }

    private void OpenTab(GameObject TabToOpen)
    {
        SoundManager.ButtonSound?.Invoke();
        TabToOpen.SetActive(true);
    }

    private void CloseTab(GameObject TabToClose)
    {
        SoundManager.ButtonSound?.Invoke();
        TabToClose.SetActive(false);
    }

    private void IncraseMoves() => _data.MoveCounter.text = (int.Parse(_data.MoveCounter.text) + 1).ToString();
    private void DecraseMoves() => _data.MoveCounter.text = (int.Parse(_data.MoveCounter.text) - 1).ToString();

    private void PerformRestartButton() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    private void PerformStagesButton()
    {
        MainMenuUI.GoToLevelWindow = true;
        SceneManager.LoadScene("MenuScene");
    }

    private void OpenGameOverTabRequest() => StartCoroutine(OpenGameOverTab());

    private IEnumerator OpenGameOverTab()
    {
        yield return new WaitForSeconds(1f);
        _data.GameOverTab.SetActive(true);
    }

    private void SetHUDData(HUDData data) => _data = data;

    public void UpdateSkin() => LevelLoader.SetSkin(LevelLoader.LevelToLoad);

    private void InitHUD()
    {
        _data.Difficulty.text = GetDifficulty();
        _data.Score.sprite = GetScore();
        _data.BestMoves.text = GetBestMoves();
        _data.LevelCounter.text = GetLevelCounter();
        if (LevelManager.GameState == GameState.Tutorial)
            _data.TutorialLabel.SetActive(true);
    }

    private string GetLevelCounter() => LevelLoader.LevelToLoad.LevelIndex.ToString();
    private string GetDifficulty() => _currentLevel.Difficulty.ToString().ToUpper();
    private Sprite GetScore()
    {
        Score bestScore = (Score)SaveManager.GetLevelDataInt(_currentLevel, Constants.BEST_SCORE);
        int bestMoves = SaveManager.GetLevelDataInt(_currentLevel, Constants.BEST_MOVES);

        if (bestScore == Score.Crown)
            return _data.ScoreSprites[_data.ScoreSprites.Length - 1];
        else if (bestMoves > 0)
            return _data.ScoreSprites[(int)bestScore + 1];
        else 
            return _data.ScoreSprites[0];
    }
    private string GetBestMoves()
    {
        int bestMoves = SaveManager.GetLevelDataInt(_currentLevel, Constants.BEST_MOVES);
        if (bestMoves == 0)
            return $"-/{_currentLevel.OptimalMoves}";
        else
            return $"{bestMoves}/{_currentLevel.OptimalMoves}";
    }

    private void ShowHintMSG() => StartCoroutine(ComingSoonMSG());

    private IEnumerator ComingSoonMSG()
    {
        float deltaTime = 0.5f;
        float alpha = 1f;

        _data.ComingSoonMSG.color = new Color(_data.ComingSoonMSG.color.r, _data.ComingSoonMSG.color.g, _data.ComingSoonMSG.color.b, alpha);
        yield return new WaitForSeconds(deltaTime);

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime;
            _data.ComingSoonMSG.color = new Color(_data.ComingSoonMSG.color.r, _data.ComingSoonMSG.color.g, _data.ComingSoonMSG.color.b, alpha);
            yield return null;
        }
    }

}
