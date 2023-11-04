using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager 
{
    public static void SaveLevelData(LevelData level)
    {
        int bestMoves = GetLevelDataInt(level, Constants.BEST_MOVES);
        if (bestMoves > level.Moves || bestMoves == 0)
            SetLevelDataInt(level, Constants.BEST_MOVES, level.Moves);
        
        Score bestScore = (Score)GetLevelDataInt(level, Constants.BEST_SCORE);
        if (bestScore < level.Score)
            SetLevelDataInt(level, Constants.BEST_SCORE, (int)level.Score);

        SetLevelDataInt(level, Constants.LEVEL_COMPLETED, 1);
    }

    public static int GetLevelDataInt(LevelData level, string intKey) => PlayerPrefs.GetInt($"{level.Theme}/{level.Difficulty}/{level.LevelIndex}/{intKey}");
    public static bool GetLevelDataBool(LevelData level, string boolKey)
    {
        //Debug.Log(PlayerPrefs.GetInt($"{level.Theme}/{level.Difficulty}/{level.LevelIndex}/{boolKey}"));
        int levelComplete = PlayerPrefs.GetInt($"{level.Theme}/{level.Difficulty}/{level.LevelIndex}/{boolKey}");
        if (levelComplete == 0) return false;
        else return true;
    }

    public static void SetLevelDataInt(LevelData level, string intKey, int value) => PlayerPrefs.SetInt($"{level.Theme}/{level.Difficulty}/{level.LevelIndex}/{intKey}", value);
}


