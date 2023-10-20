using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataHandler 
{
    public void LoadLevel(int levelIndex, out LevelData levelToLoad)
    {
        levelToLoad = Resources.Load<LevelData>($"Beginner/Level {levelIndex}");
        InstantiatePawns(levelToLoad);
        
        
    }

    private void InstantiatePawns(LevelData levelData)
    {
        for (int i = 0; i < levelData.Pawn.Length; i++)
            MonoBehaviour.Instantiate<SlidableComponent>(levelData.Pawn[i], levelData.PawnsPositions[i], levelData.PawnsRotations[i]);
        
    }
}
