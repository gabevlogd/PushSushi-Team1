using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager 
{
    public static void SaveLevelData(LevelData level, int moveCounter)
    {
        int bestMoves = GetLevelDataInt(level, Constants.BEST_MOVES);
        if (bestMoves > moveCounter || bestMoves == 0)
            SetLevelDataInt(level, Constants.BEST_MOVES, moveCounter);
        
        Score bestScore = (Score)GetLevelDataInt(level, Constants.BEST_SCORE);
        if (bestScore < level.Score)
            SetLevelDataInt(level, Constants.BEST_SCORE, (int)level.Score);
    }

    public static int GetLevelDataInt(LevelData level, string intKey) => PlayerPrefs.GetInt($"{level.Theme}/{level.Difficulty}/{level.LevelIndex}/{intKey}");
    public static void SetLevelDataInt(LevelData level, string intKey, int value) => PlayerPrefs.SetInt($"{level.Theme}/{level.Difficulty}/{level.LevelIndex}/{intKey}", value);
}
