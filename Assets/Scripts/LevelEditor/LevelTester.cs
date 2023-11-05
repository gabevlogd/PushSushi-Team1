using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//classe provvisoria necessaria ai designer
public class LevelTester : MonoBehaviour
{
    public GameObject map;
    public LevelData LevelToTest;

    private void Awake()
    {
        map.SetActive(false);
        LevelLoader.LevelToLoad = LevelToTest;
    }
}
