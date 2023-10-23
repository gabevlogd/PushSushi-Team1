using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public LevelData StartingLevel;
    public static LevelData LevelToLoad;
    private void Awake()
    {
        if (StartingLevel != null && LevelToLoad == null)
        {
            LevelToLoad = StartingLevel;
        }
    }

    public static LevelData GetLevel(Theme levelTheme, Difficulty levelDifficulty, int levelIndex) => Resources.Load<LevelData>(GetLevelPath(levelTheme, levelDifficulty, levelIndex));
    public static string GetLevelPath(Theme levelTheme, Difficulty levelDifficulty, int levelIndex) => $"{levelTheme}/{levelDifficulty}/Level {levelIndex}";
}
