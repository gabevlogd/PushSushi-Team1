using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader
{
    public static LevelData LevelToLoad;

    public static LevelData GetLevel(Theme levelTheme, Difficulty levelDifficulty, int levelIndex) => Resources.Load<LevelData>(GetLevelPath(levelTheme, levelDifficulty, levelIndex));
    public static LevelData[] GetLevels(Theme levelTheme, Difficulty levelDifficulty) => Resources.LoadAll<LevelData>(GetLevelsPath(levelTheme, levelDifficulty));
    public static void LoadLevel(Theme levelTheme, Difficulty levelDifficulty, int levelIndex, out LevelData levelToLoad)
    {
        levelToLoad = GetLevel(levelTheme, levelDifficulty, levelIndex);
        InstantiatePawns(levelToLoad);
    }

    private static string GetLevelPath(Theme levelTheme, Difficulty levelDifficulty, int levelIndex) => $"{levelTheme}/{levelDifficulty}/Level {levelIndex}";
    private static string GetLevelsPath(Theme levelTheme, Difficulty levelDifficulty) => $"{levelTheme}/{levelDifficulty}/";
    private static void InstantiatePawns(LevelData levelData)
    {
        for (int i = 0; i < levelData.Pawn.Length; i++)
            MonoBehaviour.Instantiate<SlidableComponent>(levelData.Pawn[i], levelData.PawnsPositions[i], levelData.PawnsRotations[i]);
    }
}
