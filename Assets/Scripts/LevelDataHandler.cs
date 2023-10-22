using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataHandler 
{
    public void LoadLevel(Theme levelTheme, Difficulty levelDifficulty, int levelIndex, out LevelData levelToLoad)
    {
        levelToLoad = Resources.Load<LevelData>($"{levelTheme}/{levelDifficulty}/Level {levelIndex}");
        InstantiatePawns(levelToLoad);
    }

    private void InstantiatePawns(LevelData levelData)
    {
        for (int i = 0; i < levelData.Pawn.Length; i++)
            MonoBehaviour.Instantiate<SlidableComponent>(levelData.Pawn[i], levelData.PawnsPositions[i], levelData.PawnsRotations[i]);
    }
}
