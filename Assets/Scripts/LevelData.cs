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
    public int LevelIndex;
    public int MinimumMoves;
    public int LevelScore;

}

public enum Difficulty
{
    Beginner,
    Intermediate,
    Advanced
}
