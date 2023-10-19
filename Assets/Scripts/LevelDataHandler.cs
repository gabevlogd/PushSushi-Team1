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
        for (int i = 0; i < levelData.PawnsIDs.Length; i++)
        {
            SlidableComponent pawn = Resources.Load<SlidableComponent>(levelData.PawnsIDs[i]);
            MonoBehaviour.Instantiate<SlidableComponent>(pawn, levelData.PawnsPositions[i], levelData.PawnsRotations[i]);
        }
    }
}
