using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public LevelData StartingLevel;
    public static LevelData LevelToLoad;
    private void Awake()
    {
        if (StartingLevel != null)
        {
            LevelToLoad = StartingLevel;
        }
    }
}
