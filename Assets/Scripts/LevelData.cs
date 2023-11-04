using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Level", fileName = "NewLevel")]
public class LevelData : ScriptableObject
{
    public SlidableComponent MainPawn;
    public SlidableComponent[] Pawn;
    public Vector3[] PawnsPositions;
    public Quaternion[] PawnsRotations;
    public Difficulty Difficulty;
    public Theme Theme;
    public Score Score;
    //public Score BestScore;
    public int LevelIndex;
    public int OptimalMoves;
    public int Moves;

}

public enum Difficulty
{
    Beginner,
    Intermediate,
    Advanced
}

public enum Theme
{
    Sushi,
    Penguin,
    Sweet
}

public enum Score
{
    None = -1,
    Good,
    Great,
    Perfect,
    Crown 
}
