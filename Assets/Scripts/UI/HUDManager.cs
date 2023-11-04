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
    private LevelData _currentLevel;

    private void Awake() => _currentLevel = LevelLoader.LevelToLoad;

    private void OnEnable()
    {
        SlidableComponent.OnLevelComplete += OpenGameOverTabRequest;
        ThemeSetter.OnHUDActivation += SetHUDData;
        UndoManager.OnMoveStored += IncraseMoves;
        UndoManager.OnMoveCanceled += DecraseMoves;
    }

    private void OnDisable()
    {
        SlidableComponent.OnLevelComplete -= OpenGameOverTabRequest;
        ThemeSetter.OnHUDActivation -= SetHUDData;
        UndoManager.OnMoveStored -= IncraseMoves;
        UndoManager.OnMoveCanceled -= DecraseMoves;
    }

    private void Start()
    {
        _data.Pause.onClick.AddListener(delegate { OpenTab(_data.PauseTab); });
        _data.Skins.onClick.AddListener(delegate { OpenTab(_data.SkinsTab); });
        _data.Resume.onClick.AddListener(delegate { CloseTab(_data.PauseTab); });
        _data.Close.onClick.AddListener(delegate { CloseTab(_data.SkinsTab); });
        //_data.Close.onClick.AddListener(delegate { LevelLoader.SetSkin(LevelLoader.LevelToLoad); });
        _data.Restart.onClick.AddListener(delegate { PerformRestartButton(_data.PauseTab); });
        _data.Stages.onClick.AddListener(PerformStagesButton);
        //_data.LevelCounter.text = LevelLoader.LevelToLoad.LevelIndex.ToString();
        InitHUD();
    }

    private void OpenTab(GameObject TabToOpen) => TabToOpen.SetActive(true);

    private void CloseTab(GameObject TabToClose) => TabToClose.SetActive(false);
    private void IncraseMoves() => _data.MoveCounter.text = (int.Parse(_data.MoveCounter.text) + 1).ToString();
    private void DecraseMoves() => _data.MoveCounter.text = (int.Parse(_data.MoveCounter.text) - 1).ToString();

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

    public void UpdateSkin() => LevelLoader.SetSkin(LevelLoader.LevelToLoad);

    private void InitHUD()
    {
        _data.Difficulty.text = GetDifficulty();
        _data.Score.sprite = GetScore();
        _data.BestMoves.text = GetBestMoves();
        _data.LevelCounter.text = GetLevelCounter();
    }

    private string GetLevelCounter() => LevelLoader.LevelToLoad.LevelIndex.ToString();
    private string GetDifficulty() => _currentLevel.Difficulty.ToString().ToUpper();
    private Sprite GetScore()
    {
        Score bestScore = (Score)SaveManager.GetLevelDataInt(_currentLevel, Constants.BEST_SCORE);
        if (/*_currentLevel.BestScore*/bestScore == Score.Crown)
            return _data.ScoreSprites[_data.ScoreSprites.Length - 1];
        else
            return _data.ScoreSprites[/*(int)_currentLevel.BestScore*/(int)bestScore + 1];
    }
    private string GetBestMoves()
    {
        int bestMoves = SaveManager.GetLevelDataInt(_currentLevel, Constants.BEST_MOVES);
        if (bestMoves == 0)
            return $"-/{_currentLevel.OptimalMoves}";
        else
            return $"{bestMoves}/{_currentLevel.OptimalMoves}";
    }
}
