using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader
{
    public static LevelData LevelToLoad;
    public static SlidableComponent MainPawn;

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
        int skinIndex = 0;
        for (int i = 0; i < levelData.Pawn.Length; i++)
        {
            //if player selected a skin we check if the current cycled pawn is the main pawn
            //and if it is we store its index and continue without instantiate it
            //because we'll instantiate the skin instead in SetSkin method
            if (MenuData.SelectedSkin != null)
            {
                if (levelData.Pawn[i] == levelData.MainPawn)
                {
                    skinIndex = i;
                    continue;
                }
            }

            SlidableComponent pawn = MonoBehaviour.Instantiate<SlidableComponent>(levelData.Pawn[i], levelData.PawnsPositions[i], levelData.PawnsRotations[i]);
            if (levelData.Pawn[i] == levelData.MainPawn)
                MainPawn = pawn;
        }

        SetSkin(levelData, skinIndex);
    }

    public static void SetSkin(LevelData levelData, int skinIndex = -1)
    {
        if (MenuData.SelectedSkin != null)
        {
            if (skinIndex == -1)
            {
                for(int i = 0; i < levelData.Pawn.Length; i++)
                {
                    if (levelData.Pawn[i] == levelData.MainPawn)
                    {
                        skinIndex = i;
                        MonoBehaviour.Destroy(MainPawn.gameObject);
                        break;
                    }
                }
            }
            MainPawn = MonoBehaviour.Instantiate<SlidableComponent>(MenuData.SelectedSkin, levelData.PawnsPositions[skinIndex], levelData.PawnsRotations[skinIndex]);
            if (LevelManager.GameState == GameState.Tutorial)
                TutorialManager.MainPawn = MainPawn;
        }
    }
}
