using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//classe provvisoria necessaria ai designer
public class LevelTester : MonoBehaviour
{
    public LevelData LevelToTest;

    private void Awake()
    {
        LevelLoader.LevelToLoad = LevelToTest;
    }
}
